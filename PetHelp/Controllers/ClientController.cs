using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class ClientController : ODataController
    {
        private readonly DatabaseContext _dbContext;
        private readonly INotificatorService _notificatorService;
        public ClientController(DatabaseContext dbContext, INotificatorService notificator)
        {
            _dbContext = dbContext;
            _notificatorService = notificator;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_dbContext.Clients);
        }

        public async Task<IActionResult> Get(int key)
        {
            var result = await _dbContext.Clients.Where(e => e.Id == key).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        public async Task<IActionResult> Post([FromBody] ClientDto Client)
        {
            var CPFExists = await _dbContext.Clients.Where(e => e.CPF == Client.CPF).AnyAsync();
            if (CPFExists)
            {
                _notificatorService.Notify("Cliente", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }
            var RGExists = await _dbContext.Clients.Where(e => e.RG == Client.RG).AnyAsync();
            if (RGExists)
            {
                _notificatorService.Notify("Cliente", "Já existe um cadastro com este RG");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Add(Client);

            return Created(Client);
        }

        public async Task<IActionResult> Put(int key, [FromBody] ClientDto Client)
        {
            var result = _dbContext.Clients.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                _notificatorService.Notify("Client", "Não foi possivel encontrar o Client");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            var CPFExists = await _dbContext.Clients.Where(e => e.CPF == Client.CPF).AnyAsync();
            if (CPFExists)
            {
                _notificatorService.Notify("Cliente", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }
            var RGExists = await _dbContext.Clients.Where(e => e.RG == Client.RG).AnyAsync();
            if (RGExists)
            {
                _notificatorService.Notify("Cliente", "Já existe um cadastro com este RG");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Entry(result).CurrentValues.SetValues(Client);

            return Created(Client);
        }
        public async Task<IActionResult> Delete([FromQuery] int key)
        {
            var result = await _dbContext.Clients.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                return NoContent();
            }

            var clientClientExists = await _dbContext.Adoptions.AnyAsync(e => e.ClientId == key);
            if (!clientClientExists)
            {
                _notificatorService.Notify("ClientClient", "Não é possivel deletar um cliente");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Remove(result);

            return Ok(result);
        }
    }
}
