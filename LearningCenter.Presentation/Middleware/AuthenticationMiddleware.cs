using LearningCenter.Domain.IAM.Models;
using LearningCenter.Domain.IAM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LearningCenter.Presentation.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context,ITokenService tokenService,IUserQueryService userQueryService)
    {
        // Allow anonymus access
        var isAllowAnonymous = await IsAllowAnonymousAsync(context);

        if (isAllowAnonymous)
        {
            await _next(context);
            return;
        }
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
     
        // Validate if token is present
        if (token == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("token is missing");
            return;
        }

        var userId = await tokenService.ValidateToken(token);
        
        // Token couldn't be decoded
        if (userId == null || userId == 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid token");
            return;
        }
        
        var query = new GetUserByIdQuery(userId.Value);

        var user = await userQueryService.GetUserByIdAsync(query);
        
        if( user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("User not found");
            return;
        }
        
        context.Items["User"] = user;
        
        await _next(context);
    }
    
    private Task<bool> IsAllowAnonymousAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null) return Task.FromResult(false);

        var allowAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null;

        if (!allowAnonymous)
        {
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor != null)
                allowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
                    .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));
        }

        return Task.FromResult(allowAnonymous);
    }
}