using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class AnimalController(DatabaseContext dbContext, INotificatorService notificatorService) : Microsoft.AspNetCore.OData.Routing.Controllers.ODataController
    {

        //[EnableQuery]
        //public IActionResult Get()
        //{
        //    return Ok(dbContext.Animals);
        //}

        //[EnableQuery]
        //[HttpGet("Adoptions")]
        //public IActionResult GetAdoptions()
        //{
        //    return Ok(dbContext.Adoptions);
        //}

        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Animals.Where(e => e.Id == key).ToListAsync();

            if (result == null)
            {
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            return Ok(result);
        }

        [Authorize(PetHelpRoles.Admin)]
        [Authorize(PetHelpRoles.Employee)]
        public async Task<IActionResult> Post([FromBody] AnimalDto animal)
        {
            var AnimalExists = await dbContext.Animals.Where(e => e.Id == animal.ClinicId).AnyAsync();
            if (!AnimalExists)
            {
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var ClinicsExists = await dbContext.Clinics.Where(e => e.Id == animal.ClinicId).AnyAsync();
            if (!ClinicsExists)
            {
                notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Add(animal);

            return Created(animal);
        }

        public async Task<IActionResult> Put(int key, [FromBody] AnimalDto animal)
        {
            var result = dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var ClinicsExists = await dbContext.Clinics.Where(e => e.Id == animal.ClinicId).AnyAsync();
            if (!ClinicsExists)
            {
                notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(result).CurrentValues.SetValues(animal);

            return Created(animal);
        }
        public async Task<IActionResult> Delete([FromQuery] int key)
        {
            var result = await dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var clientAnimalExists = await dbContext.Watcheds.AnyAsync(e => e.AnimalId == key);
            if (!clientAnimalExists)
            {
                notificatorService.Notify("ClientAnimal", "Não é possivel deletar um animal já adotado");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Remove(result);

            return Ok(result);
        }
    }
}
