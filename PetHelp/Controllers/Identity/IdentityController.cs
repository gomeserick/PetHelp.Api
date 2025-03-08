using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Application.Security;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Context.Interfaces;
using System.Security.Claims;

namespace PetHelp.Controllers.Identity
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController(
        UserManager<IdentityBaseDto> userManager,
        IContext context) : ControllerBase
    {

        [HttpPut("Finish")]
        [Authorize("Default")]
        public async Task<IActionResult> UpdateUser([FromBody] FullRegistrationRequest updateDto)
        {
            var user = await userManager.FindByIdAsync(context.UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await userManager.GetRolesAsync(user);

            if(userRoles.Any())
            {
                return Forbid();
            }

            user.UserName = updateDto.Name;
            user.CPF = updateDto.CPF;
            user.RG = updateDto.RG;

            user.Address = updateDto.Address;
            user.Image = updateDto.Image;
            user.NotificationEnabled = updateDto.NotificationEnabled;
            user.RegistrationFlag = true;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Add "Registered" claim if it doesn't already exist
            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Any(c => c == PetHelpRoles.User))
            {
                var roleResult = await userManager.AddToRoleAsync(user, PetHelpRoles.User);
                if (!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
                }
            }

            return NoContent();
        }

        [HttpGet("AccessDenied")]
        [HttpPost("AccessDenied")]
        [HttpPut("AccessDenied")]
        [HttpDelete("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return Forbid();
        }
    }
}
