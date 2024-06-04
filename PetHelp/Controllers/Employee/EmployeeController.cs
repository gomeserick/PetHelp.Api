using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using System.Reflection.Metadata;

namespace PetHelp.Controllers.Employee
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(PetHelpRoles.Employee)]
    public class EmployeeController(
        DatabaseContext context, 
        UserManager<IdentityBaseDto> userStore, 
        INotificatorService notificator): ControllerBase
    {
        [HttpPost("Photo")]
        public async Task<IActionResult> UpdatePhoto([FromBody] IFormFile image)
        {
            var user = await userStore.GetUserAsync(User);
            if(image.ContentType != "image/jpeg" && image.ContentType != "image/png")
            {
                notificator.Notify("Photo", "Formato de imagem inválido");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var photo = new byte[image.Length];

            var path = Path.Combine("C:/", "PetHelp", "Images");

            Directory.CreateDirectory(path);

            System.IO.File.WriteAllBytes(Path.Combine(path, user.Id + ".jpg"), photo);



            await userStore.UpdateAsync(user);

            return Ok();
        }
    }
}
