using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;
using System.Security.Claims;

namespace PetHelp.Middlewares
{
    public class ContextMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext, IContext context, DatabaseContext dbContext, UserManager<IdentityBaseDto> manager, IMapper mapper)
        {
            var user = await manager.GetUserAsync(httpContext.User);
            if (user == null) goto next;

            var userId = await dbContext.Users.Join(dbContext.Employees, user => user.Id, employee => employee.UserId, (user, employee) => user.Id).FirstOrDefaultAsync();

            mapper.Map(user, context);

            context.IsEmployee = userId != 0;

            var claims = await manager.GetClaimsAsync(user);

            if (claims == null) goto next;

            context.Claims = claims.ToDictionary(k => k.Type, v => v.Value);

        next:
            await _next(httpContext);
        }
    }
}
