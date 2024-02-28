using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class ScheduleController(DatabaseContext dbContext, INotificatorService notificatorService) : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(dbContext.Employees);
        }
        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Schedules.Where(e => e.Id == key).ToListAsync();
            if (result == null)
            {
                notificatorService.Notify("Schedule", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            return Ok(result);
        }
        public async Task<IActionResult> Post([FromBody] ScheduleDto schedule)
        {
            var employeeExists = await dbContext.Employees.Where(e => e.Id == schedule.EmployeeId).AnyAsync();
            var clientExists = await dbContext.Clients.Where(e => e.Id == schedule.ClientId).AnyAsync();
            var animalExists = await dbContext.Animals.Where(e => e.Id == schedule.AnimalId).AnyAsync();

            if(!employeeExists)
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");

            if(!clientExists)
                notificatorService.Notify("Clinic", "Não foi possivel encontrar a clinica");

            if(!animalExists)
                notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");

            if(notificatorService.HasNotifications())
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));

            var dateQuery = dbContext.Schedules
                .Where(e => e.Date >= schedule.Date)
                .Where(e => e.Date <= schedule.Date.AddMinutes(e.Duration));

            var clientOccupied = await dateQuery
                .Where(e => e.ClientId == schedule.ClientId)
                .AnyAsync();

            var employeeOccupied = await dateQuery
                .Where(e => e.EmployeeId == schedule.EmployeeId)
                .AnyAsync();

            var animalOccupied = await dateQuery
                .Where(e => e.AnimalId == schedule.AnimalId)
                .AnyAsync();

            if(clientOccupied)
            {
                notificatorService.Notify("Schedule", "Esta data já está em co");
            }

            if(employeeOccupied)
            {
                notificatorService.Notify("Schedule", "Funcionário ocupado");
            }

            if(animalOccupied)
            {
                notificatorService.Notify("Schedule", "Animal ocupado");
            }

            if(notificatorService.HasNotifications())
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));

            dbContext.Add(schedule);

            return Created(schedule);
        }
        public async Task<IActionResult> Delete(int key)
        {
            var result = await dbContext.Schedules.FirstOrDefaultAsync(e => e.Id == key);
            if (result == null)
            {
                notificatorService.Notify("Schedule", "Não foi possivel encontrar o Agendamento");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            dbContext.Remove(result);
            return NoContent();
        }
    }
}
