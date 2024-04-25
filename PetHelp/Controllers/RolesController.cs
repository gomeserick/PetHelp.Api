using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class RolesController(DatabaseContext context, INotificatorService notificator): ODataController
    {
        [EnableQuery]
        public ActionResult<IdentityRole<int>> Get()
        {
            return Ok(context.Roles);
        }

        public async Task<ActionResult<IdentityRole<int>>> Get(int key)
        {
            var result = await context.Roles.Where(e => e.Id == key).ToListAsync();

            if (result == null)
            {
                notificator.Notify("Role", "Não foi possivel encontrar a função");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            return Ok(result);
        }
    }
}
