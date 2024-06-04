using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;
using System.Security.Claims;

namespace PetHelp.Controllers.SysAdm
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(PetHelpRoles.Admin)]
    public class SysAdmController(
        IUserClaimStore<IdentityBaseDto> claimStore,
        UserManager<IdentityBaseDto> userManager,
        DatabaseContext context,
        NotificatorService notificator): ControllerBase
    {
        [HttpPut("Employee/Claim")]
        public async Task<IActionResult> UpdateClaim([FromQuery] int userId, [FromBody] IEnumerable<string> claimList)
        {
            var user = await claimStore.FindByIdAsync(userId.ToString(), CancellationToken.None);
            if (user == null)
            {
                notificator.Notify("User", "Usuário não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            var claim = claimList.Select(claim => new Claim("Employee", claim)).ToList();

            var userClaims = await claimStore.GetClaimsAsync(user, CancellationToken.None);

            await claimStore.RemoveClaimsAsync(user, userClaims, CancellationToken.None);

            await claimStore.AddClaimsAsync(user, claim, CancellationToken.None);
            
            return Ok();
        }

        [HttpPut("Employee/Reset")]
        public async Task<IActionResult> ResetEmployeePassword([FromQuery] int userId, [FromBody] IEnumerable<string> claimList)
        {
            var user = await claimStore.FindByIdAsync(userId.ToString(), CancellationToken.None);
            if (user == null)
            {
                notificator.Notify("User", "Usuário não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            await userManager.RemovePasswordAsync(user);

            return Ok();
        }

        [HttpPut("Employee/Promote")]
        public async Task<IActionResult> PromoteEmployee([FromQuery] int userId)
        {
            var user = await claimStore.FindByIdAsync(userId.ToString(), CancellationToken.None);
            if (user == null)
            {
                notificator.Notify("User", "Usuário não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            await userManager.AddToRoleAsync(user, PetHelpRoles.Employee);
            await userManager.RemoveFromRoleAsync(user, PetHelpRoles.Client);

            return Ok();
        }

        [HttpPut("Employee/Remove")]
        public async Task<IActionResult> RemoveEmployee([FromQuery] int userId)
        {
            var user = await claimStore.FindByIdAsync(userId.ToString(), CancellationToken.None);
            if (user == null)
            {
                notificator.Notify("User", "Usuário não encontrado");
                return ValidationProblem(new ValidationProblemDetails(notificator.GetNotifications()));
            }

            await userManager.RemoveFromRoleAsync(user, PetHelpRoles.Employee);
            context.Remove(user);

            return Ok();
        }
    }
}
