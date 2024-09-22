using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicController(
        DatabaseContext dbContext, 
        INotificatorService notificatorService,
        IMapper mapper
        ) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] ClinicDto clinic)
        {
            var ClinicsExists = await dbContext.Clinics.Where(e => e.Id == clinic.Id).AnyAsync();
            if (ClinicsExists)
            {
                notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Add(clinic);

            return Created(clinic.Id.ToString(), clinic);
        }
        [HttpPut("Update/{key}")]
        public async Task<IActionResult> Put(int key, [FromBody] ClinicDto clinic)
        {
            var clinicExists = await dbContext.Clinics.AnyAsync(e => e.Id == key);

            if (!clinicExists)
            {
                notificatorService.Notify("Clinic", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(clinicExists).CurrentValues.SetValues(clinic);

            return Created(clinic.Id.ToString(), clinic);
        }
        [HttpDelete("Delete/{key}")]
        public async Task<IActionResult> Delete(int key)
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

            return NoContent();
        }

        [HttpPost("Apointment/Create")]
        public async Task<IActionResult> CreateApointment([FromBody] ApointmentRequest request)
        {
            var result = await dbContext.Clinics
                .AnyAsync(e => e.Id == request.ClinicId);


            var a = await dbContext.Clinics
                .FirstOrDefaultAsync(e => e.Id == request.ClinicId);

            if (result)
            {
                notificatorService.Notify("Clinic", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if (result)
            {
                notificatorService.Notify("ClientAnimal", "Não é possivel deletar uma clínica com animais adotados");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var animalApoitments = await dbContext.Apointments
                .Include(e => e.Details.Where(e => request.Animals.Contains(e.AnimalId)))
                .Where(e => e.Details.Any())
                .AnyAsync(e => e.Date >= request.Date && e.Date <= request.Date + request.Duration);

            if (animalApoitments)
            {
                notificatorService.Notify("Apointment", "Não é possivel agendar um consulta para um animal que já tem um consulta marcado");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var apointment = mapper.Map<ApointmentHeaderDto>(request);

            dbContext.Add(apointment);

            return Created(apointment.Id.ToString(), result);
        }
    }
}
