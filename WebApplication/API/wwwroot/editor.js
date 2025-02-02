const inputText = document.getElementById('inputText');
const outputText = document.getElementById('outputText');
const convertBtn = document.getElementById('convertBtn');
const saveBtn = document.getElementById('saveBtn');
const url = window.location.href.split('/');
const id = url[url.length - 1];
loadContent().then()

// Обработка нажатия на кнопку "Конвертировать"
convertBtn.addEventListener('click', async () => {
    const text = inputText.value;
    await convert(text);
});

// Обработка нажатия на кнопку "Сохранить"
saveBtn.addEventListener('click', async () => {
    const text = {text:inputText.value};

    const response2 = await fetch(`document/access/update/${id}`,{
        method: 'PATCH',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(text),
    });
    if (!response2.ok) {
        saveBtn.disabled = true;
    }
});

async function loadContent() {
    const response = await fetch(`document/access/${id}`);
    let data;
    if (response.ok) {
        data = await response.text();
        inputText.innerText = data;
        await convert(data);
    }
    else{
        document.body.textContent = '403';
    }

    const md = {text:data};
    const response2 = await fetch(`document/access/update/${id}`,{
        method: 'PATCH',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(md),
    });
    if (!response2.ok) {
        saveBtn.disabled = true;
    }
}
async function convert(text) {
    const md = {text: text};
    const response = await fetch('md/convert', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(md),
    });

    const html = await response.text();
    outputText.innerHTML = html;
}