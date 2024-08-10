using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers.Private
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("EndUser")]
    public class ScheduleController(DatabaseContext dbcontext, INotificatorService notificator): ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request)
        {
            var employeeExists = dbcontext.Employees.Any(e => e.Id == request.EmployeeId);
            if(!employeeExists)
            {
                notificator.Notify("Employee", "Funcionário não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var animalExists = dbcontext.Animals.Any(e => e.Id == request.AnimalId);
            if(!animalExists)
            {
                notificator.Notify("Animal", "Animal não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var isEmployeeBusy = await dbcontext.Schedules.AnyAsync(e => e.EmployeeId == request.EmployeeId && e.Date <= request.Date && e.Date > request.Date.AddMinutes(-30));
            if(isEmployeeBusy)
            {
                notificator.Notify("Employee", "Funcionário não disponível");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var isAnimalBusy = await dbcontext.Schedules.AnyAsync(e => e.AnimalId == request.AnimalId && e.Date <= request.Date && e.Date > request.Date.AddMinutes(-30));
            if(isAnimalBusy)
            {
                notificator.Notify("Animal", "Animal não disponível");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var endDate = request.Date.AddMinutes(30);

            var schedule = new ScheduleDto
            {
                Date = request.Date,
                EmployeeId = request.EmployeeId,
                AnimalId = request.AnimalId,
                Duration = endDate - request.Date
            };

            dbcontext.Schedules.Add(schedule);

            return Ok();
        }

        [HttpPost("ReSchedule({key})")]
        public async Task<IActionResult> Update([FromBody] DateTime newDate, int key)
        {
            var scheduleExists = await dbcontext.Schedules.AnyAsync(e => e.Id == key);
            if(!scheduleExists)
            {
                notificator.Notify("Schedule", "Agendamento não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var schedule = await dbcontext.Schedules.FirstOrDefaultAsync(e => e.Id == key);

            var isEmployeeBusy = await dbcontext.Schedules.AnyAsync(e => e.EmployeeId == schedule.EmployeeId && e.Date <= schedule.Date && e.Date > schedule.Date.AddMinutes(-30) && e.Id != key);
            if (isEmployeeBusy)
            {
                notificator.Notify("Employee", "Funcionário não disponível");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var isAnimalBusy = await dbcontext.Schedules.AnyAsync(e => e.AnimalId == schedule.AnimalId && e.Date <= schedule.Date && e.Date > schedule.Date.AddMinutes(-30) && e.Id != key);
            if (isAnimalBusy)
            {
                notificator.Notify("Animal", "Animal não disponível");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            schedule.Date = newDate;

            return Ok();
        }

        [HttpPost("Cancel({key})")]
        public async Task<IActionResult> Delete(int key)
        {
            var scheduleExists = await dbcontext.Schedules.AnyAsync(e => e.Id == key);
            if (!scheduleExists)
            {
                notificator.Notify("Schedule", "Agendamento não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var schedule = await dbcontext.Schedules.FirstOrDefaultAsync(e => e.Id == key);

            dbcontext.Schedules.Where(e => e.Id == key).ExecuteDelete();

            return Ok();
        }
    }
}
