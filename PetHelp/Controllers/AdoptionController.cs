using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Dtos;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using System.Linq;


namespace PetHelp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdoptionController(
        INotificatorService notificator, 
        DatabaseContext dbContext, 
        IContext context,
        IMapper mapper): ControllerBase
    {
        [Authorize(PetHelpRoles.Employee)]
        [Authorize(PetHelpRoles.Admin)]
        [HttpPut("Confirm")]
        public async Task<ActionResult> Post(int key)
        {
            var adoption = await dbContext.Adoptions
                .Include(e => e.AdoptionDetails)
                .ThenInclude(e => e.Animal)
                .ThenInclude(e => e.Schedules.Where(e => !e.Cancelled && e.Date > DateTime.Now))
                .Include(e => e.AdoptionDetails)
                .ThenInclude(e => e.Animal)
                .ThenInclude(e => e.WatchedList)
                .FirstOrDefaultAsync(e => e.Id == key && e.Status == AdoptionStatus.Pending);
            if (adoption == null)
            {
                notificator.Notify("Adoption", "Adoção não encontrada, iniciada ou já finalizada");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            adoption.Status = AdoptionStatus.Confirmed;
            var userId = adoption.AdoptionDetails.Select(e => e.UserId).FirstOrDefault();

            var animals = adoption.AdoptionDetails.Select(e => e.Animal).ToList();

            await dbContext.Animals
                .Intersect(animals)
                .ExecuteUpdateAsync(e => e.SetProperty(e => e.UserId, userId));

            await dbContext.Schedules
                .Intersect(animals.SelectMany(e => e.Schedules))
                .ExecuteUpdateAsync(e => 
                e.SetProperty(e => e.Cancelled, true)
                 .SetProperty(e => e.CancellationReason, "Animal Adotado")
                );

            await dbContext.Watcheds.Intersect(animals.SelectMany(e => e.WatchedList)).ExecuteDeleteAsync();

            return Created();
        }

        [Authorize(PetHelpRoles.Employee)]
        [Authorize(PetHelpRoles.Admin)]
        [HttpPut("Create")]
        public async Task<ActionResult> Post([FromBody] AdoptionCreationRequest adoptionHeader)
        {
            if (adoptionHeader == null)
            {
                notificator.Notify("Adoption", "Adoção não encontrada ou não iniciada");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var animalIds = adoptionHeader.AdoptionDetails.Select(e => e.AnimalId).ToList();

            var animalsAvailable = await dbContext.AdoptionDetails
                .Include(e => e.AdoptionHeader)
                .Where(e => animalIds.Contains(e.AnimalId))
                .AnyAsync(e => e.AdoptionHeader.Status != AdoptionStatus.Cancelled);

            if (animalsAvailable) 
            {
                notificator.Notify("Adoption", "Um ou mais animais já estão em processo de adoção");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var adoption = mapper.Map<AdoptionHeaderDto>(adoptionHeader);
            adoption.UserId = context.UserId;

            adoption.AdoptionDetails = adoption.AdoptionDetails.Select(e =>
            {
                e.UserId = context.UserId;
                return e;
            }).AsEnumerable();

            dbContext.Adoptions.Add(adoption);

            return Created();
        }
    }
}
