using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using System.Security.Claims;

namespace PetHelp.Controllers
{
    [Authorize(PetHelpRoles.Employee)]
    [Authorize(PetHelpRoles.Admin)]
    public class EmployeeController(
        DatabaseContext dbContext,
        RoleManager<IdentityRole<int>> roleManager,
        UserManager<IdentityBaseDto> userManager,
        INotificatorService notificatorService) : ODataController
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

        [Authorize(PetHelpRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            dbContext.Add(employee);
            var user = await userManager.FindByIdAsync(employee.Id.ToString());
            await userManager.AddToRoleAsync(user, PetHelpRoles.Employee);
            
            return Created(employee);
        }

        [Authorize(PetHelpRoles.Employee)]
        public async Task<IActionResult> Put([FromBody] EmployeeDto employee)
        {
            var result = await dbContext.Employees.FirstOrDefaultAsync(e => e.UserId == e.UserId);
            if (result == null)
            {
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if(employee.Id != (await userManager.GetUserAsync(User)).Id)
            {
                notificatorService.Notify("Employee", "Não foi possivel encontrar o funcionário");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Update(employee);
            return Updated(employee);
        }

        [Authorize(PetHelpRoles.Admin)]
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
