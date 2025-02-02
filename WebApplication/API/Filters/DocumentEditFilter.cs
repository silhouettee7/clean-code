using Application.Abstract.Services;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class DocumentEditFilter(IDocumentAccessService accessService): IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var document = context.HttpContext.Items["Document"] as Document;
        if (document == null) return;
        if (context.HttpContext.Items["UserId"] == null)
        {
            if (document.AccessType == AccessType.PublicEdit)
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
}