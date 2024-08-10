using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers.Odata
{

    public class PublicController(DatabaseContext dbContext, INotificatorService notificator) : ODataController
    {
        [EnableQuery]
        [HttpGet("odata/Animal")]
        public IActionResult GetAnimals()
        {
            return Ok(dbContext.Animals);
        }

        [EnableQuery]
        [HttpGet("odata/Animal({key})")]
        public async Task<IActionResult> GetAnimal(int key)
        {
            var result = await dbContext.Animals.Where(e => e.Id == key && e.UserId != null).ToListAsync();

            if (result == null)
            {
                notificator.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            return Ok(result);
        }

        [EnableQuery]
        [HttpGet("odata/Clinic")]
        public IActionResult GetClinics()
        {
            return Ok(dbContext.Clinics);
        }
    }
}
