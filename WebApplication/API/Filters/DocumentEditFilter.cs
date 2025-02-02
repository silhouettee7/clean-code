using Application.Abstract.Services;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class DocumentEditFilter(IDocumentAccessService accessService, IJwtWorker worker): IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var document = context.HttpContext.Items["Document"] as Document;
        if (document == null) return;
        if (context.HttpContext.Items["UserId"] == null)
        {
            if (document.AccessType == AccessType.PublicEdit || TryAuthorize(context))
            {
                await next();
            }
            else
            {
                context.Result = new ForbidResult();
            }
            
            return;
        }
        var userId = Guid.Parse(context.HttpContext.Items["UserId"].ToString());
        
        var isAccessProvided = await accessService
            .TryProvideAccessEditToUser(userId, document.Id);

        if (!isAccessProvided)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }

    private bool TryAuthorize(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Cookies["simply-cookies"];
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        var validateTokenResult = worker.ValidateToken(token);
        if (validateTokenResult.isSuccess)
        {
            return true;
        }

        return false;
    }
}