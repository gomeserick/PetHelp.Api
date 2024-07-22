using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class ClinicController(DatabaseContext dbContext, INotificatorService notificatorService) : Microsoft.AspNetCore.OData.Routing.Controllers.ODataController
    {
        //[EnableQuery]
        //public IActionResult Get()
        //{
        //    return Ok(dbContext.Animals);
        //}

        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Clinics.Where(e => e.Id == key).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        public async Task<IActionResult> Post([FromBody] ClinicDto clinic)
        {
            var ClinicsExists = await dbContext.Clinics.Where(e => e.Id == clinic.Id).AnyAsync();
            if (ClinicsExists)
            {
                notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Add(clinic);

            return Created(clinic);
        }

        public async Task<IActionResult> Put(int key, [FromBody] ClinicDto clinic)
        {
            var clinicExists = await dbContext.Clinics.AnyAsync(e => e.Id == key);

            if (!clinicExists)
            {
                notificatorService.Notify("Clinic", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(clinicExists).CurrentValues.SetValues(clinic);

            return Created(clinic);
        }
        public async Task<IActionResult> Delete([FromQuery] int key)
        {
            var result = await dbContext.Clinics
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificatorService.Notify("Clinic", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if (result.Animals.Any())
            {
                notificatorService.Notify("ClientAnimal", "Não é possivel deletar uma clínica com animais adotados");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Remove(result);

            return Ok(result);
        }
    }
}
