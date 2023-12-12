using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using PetHelp.Services.verificator;

namespace PetHelp.Controllers
{
    public class AnimalController : ODataController
    {
        private readonly DatabaseContext _dbContext;
        private readonly INotificatorService _notificatorService;
        public AnimalController(DatabaseContext dbContext, INotificatorService notificatorService)
        {
            _dbContext = dbContext;
            _notificatorService = notificatorService;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_dbContext.Animals);
        }

        public async Task<IActionResult> Get(int key)
        {
            var result = await _dbContext.Adoptions.Where(e => e.Id == key).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        public async Task<IActionResult> Post([FromBody] AnimalDto animal)
        {
            var ClinicsExists = await _dbContext.Clinics.Where(e => e.Id == animal.ClinicId).AnyAsync();
            if (!ClinicsExists)
            {
                _notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Add(animal);

            return Created(animal);
        }

        public async Task<IActionResult> Put(int key, [FromBody] AnimalDto animal)
        {
            var result = _dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                _notificatorService.Notify("Animal", "Não foi possivel encontrar o animal");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            var ClinicsExists = await _dbContext.Clinics.Where(e => e.Id == animal.ClinicId).AnyAsync();
            if (!ClinicsExists)
            {
                _notificatorService.Notify("Clinica", "Não foi possivel encontrar a clínica");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Entry(result).CurrentValues.SetValues(animal);

            return Created(animal);
        }
        public async Task<IActionResult> Delete([FromQuery] int key)
        {
            var result = await _dbContext.Animals.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                return NoContent();
            }

            var clientAnimalExists = await _dbContext.ClientAnimals.AnyAsync(e => e.AnimalId == key);
            if (!clientAnimalExists)
            {
                _notificatorService.Notify("ClientAnimal", "Não é possivel deletar um animal já adotado");
                return ValidationProblem(new ValidationProblemDetails(_notificatorService.GetNotification()));
            }

            _dbContext.Remove(result);

            return Ok(result);
        }
    }
}
