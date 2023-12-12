using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientAnimalController: ControllerBase
    {

        private readonly DatabaseContext _dbContext;
        private readonly INotificatorService _notificatorService;
        public ClientAnimalController(DatabaseContext dbContext, INotificatorService notificator)
        {
            _dbContext = dbContext;
            _notificatorService = notificator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.ClientAnimals.ToListAsync());
        }

        public async Task<IActionResult> Post([FromBody] ClientAnimalDto ClientAnimal)
        {
            var relationShipExists = await _dbContext.ClientAnimals
                .Where(e => e.AdoptionId == ClientAnimal.AdoptionId &&
                            e.ClientId == ClientAnimal.ClientId &&
                            e.AnimalId == ClientAnimal.AnimalId)
                .AnyAsync();
            if (relationShipExists)
            {
                _notificatorService.Notify("ClientAnimal", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Add(ClientAnimal);

            return Created("", ClientAnimal);
        }

        public async Task<IActionResult> Delete([FromQuery] ClientAnimalDto ClientAnimal)
        {
            var CPFExists = await _dbContext.ClientAnimals
                .Where(e => e.AdoptionId == ClientAnimal.AdoptionId &&
                            e.ClientId == ClientAnimal.ClientId &&
                            e.AnimalId == ClientAnimal.AnimalId)
                .AnyAsync();
            if (CPFExists)
            {
                _notificatorService.Notify("ClientAnimal", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Remove(ClientAnimal);

            return NoContent();
        }
    }
}
