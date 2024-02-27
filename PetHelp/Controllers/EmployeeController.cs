using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class EmployeeController(DatabaseContext dbContext, INotificatorService notificatorService) : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(dbContext.Employees);
        }
        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Employees.Where(e => e.Id == key).ToListAsync();
            if (result == null)
            {
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            return Ok(result);
        }
        public IActionResult Post([FromBody] EmployeeDto employee)
        {
            dbContext.Add(employee);
            
            return Created(employee);
        }
        public async Task<IActionResult> Put(int key, [FromBody] EmployeeDto employee)
        {
            var result = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == key);
            if (result == null)
            {
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            dbContext.Update(employee);
            return Updated(employee);
        }
        public async Task<IActionResult> Delete(int key)
        {
            var result = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == key);
            if (result == null)
            {
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            dbContext.Remove(result);
            return NoContent();
        }
    }
}
