using Application.Abstract.Repositories;
using Application.Abstract.Services;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class DocumentAccessFilter(
    IDocumentRepository documentRepository,
    IJwtWorker worker): IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var document = context.HttpContext.Items["Document"] as Document;
        if (document!.AccessType == AccessType.Private)
        {
            var token = context.HttpContext.Request.Cookies["simply-cookies"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult("token is empty");
                return;
            }
            var validateTokenResult = worker.ValidateToken(token);
            if (validateTokenResult.isSuccess)
            {
                context.HttpContext.Items["UserId"] = validateTokenResult.userId;
                await next();
            }
            else
            {
                context.Result = new UnauthorizedObjectResult("token is invalid");
            }

            return;
        }
        await next();
    }
}