using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetHelp.Dtos;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    public class ClientController(DatabaseContext dbContext, INotificatorService notificatorService) : ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(dbContext.Clients);
        }

        public async Task<IActionResult> Get(int key)
        {
            var result = await dbContext.Clients.Where(e => e.Id == key).ToListAsync();

            if (result == null)
            {
                notificatorService.Notify("Cliente", "Não foi possível encontrar o cliente");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            return Ok(result);
        }

        public async Task<IActionResult> Post([FromBody] ClientDto Client)
        {
            var CPFExists = await dbContext.Clients.Where(e => e.CPF == Client.CPF).AnyAsync();
            if (CPFExists)
            {
                notificatorService.Notify("Cliente", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }
            var RGExists = await dbContext.Clients.Where(e => e.RG == Client.RG).AnyAsync();
            if (RGExists)
            {
                notificatorService.Notify("Cliente", "Já existe um cadastro com este RG");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Add(Client);

            return Created(Client);
        }

        public async Task<IActionResult> Put(int key, [FromBody] ClientDto Client)
        {
            var result = dbContext.Clients.FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificatorService.Notify("Client", "Não foi possivel encontrar o Cliente");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var CPFExists = await dbContext.Clients.Where(e => e.CPF == Client.CPF && e.Id != key).AnyAsync();
            if (CPFExists)
            {
                notificatorService.Notify("Cliente", "Já existe um cadastro com este CPF");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            var RGExists = await dbContext.Clients.Where(e => e.RG == Client.RG && e.Id != key).AnyAsync();
            if (RGExists)
            {
                notificatorService.Notify("Cliente", "Já existe um cadastro com este RG");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Entry(result).CurrentValues.SetValues(Client);

            return Created(Client);
        }
        public async Task<IActionResult> Delete([FromQuery] int key)
        {
            var result = await dbContext.Clients
                .Include(e => e.Animals)
                .Include(e => e.Adoptions)
                .FirstOrDefaultAsync(e => e.Id == key);

            if (result == null)
            {
                notificatorService.Notify("Client", "Não foi possivel encontrar o Cliente");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if (result.Animals.Any())
            {
                notificatorService.Notify("Animal", "Não é possivel deletar um cliente que já adotou um animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            if (result.Adoptions.Any())
            {
                notificatorService.Notify("Adoption", "Não é possivel deletar um cliente que já adotou um animal");
                return ValidationProblem(new ValidationProblemDetails(notificatorService.GetNotifications()));
            }

            dbContext.Remove(result);

            return Ok(result);
        }
    }
}
