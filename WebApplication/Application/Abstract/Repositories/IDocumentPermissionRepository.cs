using Domain.Enums;
using Domain.Models;

namespace Application.Abstract.Repositories;

public interface IDocumentPermissionRepository
{
    Task<bool> CreateDocumentPermission(Guid documentId, Guid userId, AccessLevel accessLevel);
    Task<bool> UpdateDocumentPermission(Guid documentId, Guid userId, AccessLevel accessLevel);
    Task<DocumentPermission?> GetDocumentPermission(Guid userId, Guid documentId);  
}