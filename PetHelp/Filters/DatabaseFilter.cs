using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Filters
{
    public class DatabaseFilter(INotificatorService notificator, DatabaseContext dbContext) : IAsyncResultFilter
    {
        public async Task OnResultExecutedAsync(ResultExecutedContext context)
        {
            
        }

#pragma warning disable CS1998
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
#pragma warning restore CS1998
        {
            if (notificator.HasNotifications())
            {
                await next();
                return;
            }

            await dbContext.SaveChangesAsync();

            await next();
        }
    }
}
