using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Context.Interfaces;
using System.Security.Claims;

namespace PetHelp.Middlewares
{
    public class ContextMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext, IContext context, UserManager<IdentityBaseDto> manager, IMapper mapper)
        {
            var user = await manager.GetUserAsync(httpContext.User);
            if (user == null) goto next;

            mapper.Map(user, context);

            var claims = await manager.GetClaimsAsync(user);

            if (claims == null) goto next;

            context.Claims = claims.ToDictionary(k => k.Type, v => v.Value);

        next:
            await _next(httpContext);
        }
    }
}
