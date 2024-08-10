using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalController(
        DatabaseContext dbContext, 
        INotificatorService notificatorService,
        IContext context) : ControllerBase
    {


        [Authorize(PetHelpRoles.Admin)]
        [Authorize(PetHelpRoles.Employee)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAnimal([FromBody] AnimalDto animal)
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

            return Created(animal.Id.ToString(), animal);
        }

        [HttpPut("Update/{key}")]
        [Authorize]
        public async Task<IActionResult> UpdateAnimal(int key, [FromBody] AnimalRequest request)
        {
            var result = dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var ClinicsExists = await dbContext.Clinics.Where(e => e.Id == request.ClinicId).AnyAsync();
            if (!ClinicsExists)
            {
                notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(result).CurrentValues.SetValues(request);

            return Ok(result);
        }

        [HttpPut("ReportDeath/{key}")]
        [Authorize]
        public async Task<IActionResult> ReportDeath(int key)
        {
            var result = await dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key && e.UserId == context.UserId);

            if (result == null)
            {
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if(result.Alive == false)
            {
                notificatorService.Notify("Animal", "O animal já faleceu");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            result.Alive = false;

            await dbContext.Watcheds.Where(e => e.AnimalId == key).ExecuteDeleteAsync();
            await dbContext.Schedules
                .Where(e => !e.Cancelled && e.Date > DateTime.Now && e.AnimalId == key)
                .ExecuteUpdateAsync(s => 
                    s.SetProperty(s => s.Cancelled, true)
                     .SetProperty(s => s.CancellationReason, "O Animal Faleceu"));

            dbContext.Remove(result);

            return NoContent();
        }

        [HttpPost("Vaccine")]
        [Authorize]
        public IActionResult Vaccine(VaccineDto vaccine)
        {
            var result = dbContext.Add(vaccine).Entity;

            return Created(result.Id.ToString(), result);
        }
    }
}
