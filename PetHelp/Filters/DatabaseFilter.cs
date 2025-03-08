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

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
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
