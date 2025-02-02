using Application.Abstract.Repositories;
using Domain.Enums;
using Domain.Models;
using Persistence;
using Persistence.Entities;

namespace Application.Repositories;

public class DocumentPermissionRepository(AppDbContext context): IDocumentPermissionRepository
{
    public async Task<bool> CreateDocumentPermission(Guid documentId, Guid userId, AccessLevel accessLevel)
    {
        try
        {
            var permissionEntity = new DocumentPermissionEntity
            {
                AccessLevelId = (int)accessLevel,
                UserId = userId,
                DocumentId = documentId,
                DocumentPermissionId = Guid.NewGuid()
            };
            await context.Permissions.AddAsync(permissionEntity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateDocumentPermission(Guid documentId, Guid userId, AccessLevel accessLevel)
    {
        throw new NotImplementedException();
    }

    public async Task<DocumentPermission?> GetDocumentPermission(Guid userId, Guid documentId)
    {
        var permissionEntity = context.Permissions.FirstOrDefault(p => p.DocumentId == documentId && p.UserId == userId);
        if (permissionEntity == null)
        {
            return null;
        }

        return new DocumentPermission
        {
            DocumentId = permissionEntity.DocumentId,
            UserId = permissionEntity.UserId,
            AccessLevel = (AccessLevel)permissionEntity.AccessLevelId,
        };
    }
    
    
}