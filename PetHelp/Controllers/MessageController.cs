using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class MessageController(DatabaseContext dbContext, INotificatorService notificatorService) : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(dbContext.Employees);
        }
        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Messages.Where(e => e.Id == key).ToListAsync();
            if (result == null)
            {
                notificatorService.Notify("Messsage", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            return Ok(result);
        }
        public async Task<IActionResult> Post([FromBody] MessageDto Message)
        {
            var employeeExists = await dbContext.Employees.Where(e => e.Id == Message.EmployeeId).AnyAsync();
            var clientExists = await dbContext.Clients.Where(e => e.Id == Message.UserId).AnyAsync();

            if (!employeeExists)
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");

            if (!clientExists)
                notificatorService.Notify("Clinic", "Não foi possivel encontrar a clinica");

            if (notificatorService.HasNotifications())
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));

            dbContext.Add(Message);

            return Created(Message);
        }
        public async Task<IActionResult> Delete(int key)
        {
            var result = await dbContext.Messages.FirstOrDefaultAsync(e => e.Id == key);
            if (result == null)
            {
                notificatorService.Notify("Messsage", "Não foi possivel encontrar o Agendamento");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            dbContext.Remove(result);
            return NoContent();
        }
    }
}
