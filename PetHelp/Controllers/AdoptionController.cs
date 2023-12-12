using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class AdoptionController: ODataController
    {
        private readonly INotificatorService _notificatorService;
        private readonly DatabaseContext _dbContext;

        public AdoptionController(INotificatorService notificatorService, DatabaseContext dbContext)
        {
            _notificatorService = notificatorService;
            _dbContext = dbContext;
        }
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_dbContext.Adoptions);
        }
        public async Task<IActionResult> Get(int key)
        {
            var result = await _dbContext.Adoptions.Where(e => e.Id == key).ToListAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        public async Task<ActionResult> Post([FromBody] AdoptionDto adoption)
        {
            var clientExists = await _dbContext.Clients.Where(e => e.Id == adoption.ClientId).AnyAsync();
            if (!clientExists)
            {
                _notificatorService.Notify("Cliente", "Não foi possivel encontrar o cliente da adoção");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            var employeeExists = await  _dbContext.Employees.Where(e => e.Id == adoption.EmployeeId).AnyAsync();
            if (!employeeExists)
            {
                _notificatorService.Notify("Funcionário", "Não foi possivel encontrar o funcionário da adoção");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Add(adoption);

            return Created(adoption);
        }
        public async Task<ActionResult> Put([FromQuery] int key, [FromBody] AdoptionDto adoption)
        {
            var result = _dbContext.Adoptions.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                _notificatorService.Notify("Funcionário", "Não foi possivel encontrar a adoção");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            var clientExists = await _dbContext.Clients.Where(e => e.Id == adoption.ClientId).AnyAsync();
            if (!clientExists)
            {
                _notificatorService.Notify("Cliente", "Não foi possivel encontrar o cliente da adoção");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            var employeeExists = await _dbContext.Employees.Where(e => e.Id == adoption.EmployeeId).AnyAsync();
            if (!employeeExists)
            {
                _notificatorService.Notify("Cliente", "Não foi possivel encontrar o cliente da adoção");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Entry(result).CurrentValues.SetValues(adoption);

            return Created(adoption);
        }
        public async Task<ActionResult> Delete([FromQuery] int key)
        {
            var result = await _dbContext.Adoptions.FirstOrDefaultAsync(e => e.Id == key);

            if(result == null)
            {
                return NoContent();
            }

            var clientAnimalExists = await _dbContext.ClientAnimals.AnyAsync(e => e.AdoptionId == key);
            if (!clientAnimalExists)
            {
                _notificatorService.Notify("ClientAnimal", "Não é possivel deletar uma adoção já realizada");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Remove(result);

            return Ok(result);
        }
    }
}
