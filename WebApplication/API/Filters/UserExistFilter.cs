using Application.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class UserExistFilter: IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        //context.HttpContext.User.FindFirst()
    }
}