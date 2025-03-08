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
        [HttpGet("odata/Animal/{key}")]
        public async Task<IActionResult> GetAnimal(int key)
        {
            var result = await dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key && e.UserId == null);

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

        [EnableQuery]
        [HttpGet("odata/Clinic({key})")]
        [HttpGet("odata/Clinic/{key}")]
        public async Task<IActionResult> GetClinic(int key)
        {
            var result = await dbContext.Clinics.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificator.Notify("Clínica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            return Ok(result);
        }
    }
}
