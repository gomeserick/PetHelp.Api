﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class AdoptionController(INotificatorService notificator, DatabaseContext dbContext): ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(dbContext.Adoptions);
        }
        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Adoptions.Where(e => e.Id == key).ToListAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        public async Task<ActionResult> Post([FromBody] AdoptionDto adoption)
        {
            var clientExists = await dbContext.Clients.Where(e => e.Id == adoption.ClientId).AnyAsync();
            if (!clientExists)
            {
                notificator.Notify("Cliente", "Não foi possivel encontrar o cliente da adoção");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var employeeExists = await  dbContext.Employees.Where(e => e.Id == adoption.EmployeeId).AnyAsync();
            if (!employeeExists)
            {
                notificator.Notify("Funcionário", "Não foi possivel encontrar o funcionário da adoção");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            dbContext.Add(adoption);

            return Created(adoption);
        }
        public async Task<ActionResult> Put([FromQuery] int key, [FromBody] AdoptionDto adoption)
        {
            var result = dbContext.Adoptions.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificator.Notify("Funcionário", "Não foi possivel encontrar a adoção");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var clientExists = await dbContext.Clients.Where(e => e.Id == adoption.ClientId).AnyAsync();
            if (!clientExists)
            {
                notificator.Notify("Cliente", "Não foi possivel encontrar o cliente da adoção");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var employeeExists = await dbContext.Employees.Where(e => e.Id == adoption.EmployeeId).AnyAsync();
            if (!employeeExists)
            {
                notificator.Notify("Cliente", "Não foi possivel encontrar o funcionário da adoção");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            dbContext.Entry(result).CurrentValues.SetValues(adoption);

            return Created(adoption);
        }
        public async Task<ActionResult> Delete([FromQuery] int key)
        {
            var result = await dbContext.Adoptions
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == key);

            if(result == null)
            {
                return NoContent();
            }

            if (result.Animals.Any())
            {
                notificator.Notify("aNIMAL", "Não é possivel deletar uma adoção já realizada");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            dbContext.Remove(result);

            return Ok(result);
        }
    }
}
