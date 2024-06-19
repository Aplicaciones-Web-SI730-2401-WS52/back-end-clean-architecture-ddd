using LearningCenter.Domain.IAM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningCenter.Presentation.Filters;

public class CustomAuthorizeAttribute : Attribute,IAsyncAuthorizationFilter
{
    private readonly string[] _roles;
    
    public CustomAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"] as User;
        
        if (_roles.Any() && !_roles.Contains(user.Role))
        {
            context.Result = new ForbidResult();
        }
    }
}