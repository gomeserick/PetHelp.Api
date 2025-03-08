using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;
using PetHelp.Services.Context;
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
        ApplicationContext appContext,
        IContext context) : ControllerBase
    {
        [Authorize("Sysadm")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAnimal([FromBody] AnimalDto animal)
        {
            var AnimalExists = await dbContext.Animals.Where(e => e.Id == animal.Id).AnyAsync();
            if (AnimalExists)
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
        //[Authorize]
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
        //[Authorize]
        public async Task<IActionResult> ReportDeath(int key)
        {
            var result = await dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key && e.UserId == context.UserId);

            if (result == null)
            {
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if (result.Alive == false)
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

        [HttpPost("Image/{key}")]
        public async Task<IActionResult> ChangeImage([FromBody] IFormFile image, int key)
        {
            var animal = await dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key);

            if (animal == null)
            {
                notificatorService.Notify("Animal", "Não foi possível encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var imageName = animal.Image ?? Guid.NewGuid().ToString();

            var path = Path.Combine(appContext.Files.ImagePath, imageName) + "." + image.ContentType;

            return Ok(animal);
        }

        [HttpPost("Vaccine")]
        //[Authorize]
        public IActionResult Vaccine(VaccineDto vaccine)
        {
            var result = dbContext.Add(vaccine).Entity;

            return Created(result.Id.ToString(), result);
        }

        [HttpPut("Vaccine")]
        //[Authorize]
        public IActionResult UpdateVaccine(VaccineDto vaccine)
        {
            //Generate a update for vaccine

            var vaccineExists = dbContext.Vaccines.Where(e => e.Id == vaccine.Id).Any();
            if (!vaccineExists)
            {
                notificatorService.Notify("Vaccine", "Não foi possivel encontrar a vacina");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(vaccine).CurrentValues.SetValues(vaccine);

            return Ok(vaccine);
        }

        [HttpPost("Medication")]
        //[Authorize]
        public IActionResult Medication(MedicationDto medication)
        {
            var result = dbContext.Add(medication).Entity;

            return Created(result.Id.ToString(), result);
        }

        [HttpPut("Medication")]
        //[Authorize]
        public IActionResult UpdateMedication(MedicationDto medication)
        {
            //Generate a update for medication

            var medicationExists = dbContext.Medication.Where(e => e.Id == medication.Id).Any();
            if (!medicationExists)
            {
                notificatorService.Notify("Medication", "Não foi possivel encontrar a medicação");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(medication).CurrentValues.SetValues(medication);

            return Ok(medication);
        }

        [HttpPost("Appointment")]
        //[Authorize]
        public IActionResult Vaccine(MedicationDto medication)
        {
            var result = dbContext.Add(medication).Entity;

            return Created(result.Id.ToString(), result);
        }
    }
}
