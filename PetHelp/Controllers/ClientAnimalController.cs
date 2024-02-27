using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientAnimalController(DatabaseContext dbContext, INotificatorService notificatorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.ClientAnimals.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientAnimalDto ClientAnimal)
        {
            var relationShipExists = await dbContext.ClientAnimals
                .Where(e => e.AdoptionId == ClientAnimal.AdoptionId &&
                            e.ClientId == ClientAnimal.ClientId &&
                            e.AnimalId == ClientAnimal.AnimalId)
                .AnyAsync();
            if (relationShipExists)
            {
                notificatorService.Notify("ClientAnimal", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Add(ClientAnimal);

            return Created("", ClientAnimal);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] ClientAnimalDto ClientAnimal)
        {
            var CPFExists = await dbContext.ClientAnimals
                .Where(e => e.AdoptionId == ClientAnimal.AdoptionId &&
                            e.ClientId == ClientAnimal.ClientId &&
                            e.AnimalId == ClientAnimal.AnimalId)
                .AnyAsync();
            if (CPFExists)
            {
                notificatorService.Notify("ClientAnimal", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Remove(ClientAnimal);

            return NoContent();
        }
    }
}
