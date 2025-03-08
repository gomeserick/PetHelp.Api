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
    public class WatchedController(
        INotificatorService notificator, 
        DatabaseContext dbContext, 
        IContext context): ControllerBase
    {
        [HttpPost("Add")]
        [Authorize("EndUser")]
        public ActionResult Add(int key)
        {
            var animal = dbContext.Animals.FirstOrDefault(e => e.Id == key);
            if (animal == null)
            {
                notificator.Notify("Animal", "Animal não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var watched = dbContext.Watcheds.FirstOrDefault(e => e.AnimalId == key && e.UserId == context.UserId);
            if (watched != null)
            {
                notificator.Notify("Animal", "Animal já está na lista de observação");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            dbContext.Watcheds.Add(new WatchedDto
            {
                AnimalId = key,
                CreationDate = DateTime.Now,
                UserId = context.UserId
            });

            dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("Remove")]
        [Authorize]
        public ActionResult Remove(int key)
        {
            var watched = dbContext.Watcheds.FirstOrDefault(e => e.AnimalId == key && e.UserId == context.UserId);
            if (watched == null)
            {
                notificator.Notify("Animal", "Animal não está na lista de observação");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            dbContext.Watcheds.Remove(watched);

            dbContext.SaveChanges();

            return Ok();
        }
    }
}
