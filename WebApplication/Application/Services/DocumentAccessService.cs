using Application.Abstract.Repositories;
using Application.Abstract.Services;
using Application.Repositories;
using Application.ResponseResult;
using Domain.Enums;

namespace Application.Services;

public class DocumentAccessService(
    IDocumentPermissionRepository permissionRepository,
    IDocumentRepository documentRepository): IDocumentAccessService
{
    public async Task<bool> TryProvideAccessEditToUser(Guid userId, Guid documentId)
    {
        var documentPermission = await permissionRepository
            .GetDocumentPermission(userId, documentId);
        if (documentPermission != null &&
            documentPermission.AccessLevel == AccessLevel.Edit)
        {
            return true;
        }
        var document = await documentRepository.GetDocumentById(documentId);
        if (document != null && document.UserId == userId)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> TryProvideAccessReadToUser(Guid userId, Guid documentId)
    {
        var documentPermission = await permissionRepository
            .GetDocumentPermission(userId, documentId);
        if (documentPermission != null &&
            (documentPermission.AccessLevel == AccessLevel.Edit ||
             documentPermission.AccessLevel == AccessLevel.Read))
        {
            return true;
        }
        var document = await documentRepository.GetDocumentById(documentId);
        if (document != null && document.UserId == userId)
        {
            return true;
        }

        return false;
    }

    public async Task<Result> CreateDocumentPermission(
        Guid userId, Guid documentId, AccessLevel accessLevel)
    {
        var isCreated = await permissionRepository
            .CreateDocumentPermission(documentId, userId, accessLevel);
        if (!isCreated)
        {
            return Result.Failure(new Error(
                "Can't create document permission", 
                ErrorType.ServerError));
        }
        return Result.Success();
    }
}