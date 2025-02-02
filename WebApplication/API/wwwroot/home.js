let documents = [];
let currentDocumentId = null;

const documentList = document.getElementById('documentList');
const createDocumentBtn = document.getElementById('createDocumentBtn');
const documentModal = document.getElementById('documentModal');
const closeModal = document.querySelector('.close');
const documentNameInput = document.getElementById('documentName');
const documentAccessInput = document.getElementById('documentAccess');
const modalTitle = document.getElementById('modalTitle');
const submitDocumentBtn = document.getElementById('submitDocumentBtn');

document.addEventListener('DOMContentLoaded', loadDocuments);

// Открытие модального окна для создания документа
createDocumentBtn.addEventListener('click', () => {
    currentDocumentId = null;
    documentNameInput.value = '';
    documentAccessInput.value = '2';
    modalTitle.textContent = 'Create Document';
    submitDocumentBtn.textContent = 'Create';
    documentModal.style.display = 'flex';
});

// Закрытие модального окна
closeModal.addEventListener('click', () => {
    documentModal.style.display = 'none';
});

// Закрытие модального окна при клике вне его
window.addEventListener('click', (event) => {
    if (event.target === documentModal) {
        documentModal.style.display = 'none';
    }
});

// Обработка отправки формы
submitDocumentBtn.addEventListener('click', async (event) => {
    event.preventDefault();
    const name = documentNameInput.value;
    const access = parseInt(documentAccessInput.value);

    if (currentDocumentId === null) {
        // Создание нового документа
        const newDocument = { title:name, type:access };
        await saveDocument(newDocument);
    } else {
        // Обновление существующего документа
        const document = documents.find(doc => doc.id === currentDocumentId);
        document.title = name;
        document.type = access;
        await updateDocument(document);
    }

    await renderDocuments();
    documentModal.style.display = 'none';
});

// Рендеринг списка документов
async function renderDocuments() {
    documentList.innerHTML = '';
    documents.forEach(doc => {
        const li = document.createElement('li');
        const a = document.createElement('a');
        a.textContent = doc.title;
        a.href = `/${doc.id}`;
        const documentEditBtn = document.createElement('button');
        documentEditBtn.textContent = 'Edit';
        const documentDeleteBtn = document.createElement('button');
        documentDeleteBtn.textContent = 'Delete';
        li.id = doc.id;
        li.appendChild(a);
        li.appendChild(documentEditBtn);
        li.appendChild(documentDeleteBtn);
        documentList.appendChild(li);
        documentEditBtn.addEventListener('click', (event) => {
            const id = event.currentTarget.parentElement.id;
            editDocument(id);
        })

        documentDeleteBtn.addEventListener('click', event => deleteDocument(event));
    });
}

// Редактирование документа
function editDocument(id) {
    const document = documents.find(doc => doc.id === id);
    currentDocumentId = document.id;
    documentNameInput.value = document.title;
    documentAccessInput.value = document.type;
    modalTitle.textContent = 'Edit Document';
    submitDocumentBtn.textContent = 'Update';
    documentModal.style.display = 'flex';
}

// Удаление документа
async function deleteDocument(event) {
    const id = event.currentTarget.parentElement.id;
    const response = await fetch(`api/document/delete/${id}`, {
        method: 'DELETE'
    });
    console.log('Document deleted');
    documents = documents.filter(doc => doc.id !== id);
    await renderDocuments();
}

// Сохранение документа (AJAX)
async function saveDocument(document) {
    const response = await fetch('api/document/create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(document),
    })
    const data = await response.json();
    console.log('Document saved:', data);
    const newDocument = {id: data.id, title:data.title,type:data.accessType};
    documents.push(newDocument);
}

// Обновление документа (AJAX)
async function updateDocument(document) {
    const response = await fetch(`api/document/update/${document.id}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(document),
    });
}

async function loadDocuments() {
    const response = await fetch('api/document/all', {
        method: 'GET',
    })
    const data = await response.json();
    console.log(data);
    data.forEach(document => {
        const doc = {id: document.id, title: document.title, type: document.accessType};
        documents.push(doc);
    });
    await renderDocuments();
}