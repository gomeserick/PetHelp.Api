using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Requests;
using PetHelp.Application.Contracts.Responses;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Notificator;

namespace PetHelp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class IdentityController(
        SignInManager<IdentityBaseDto> signInManager,
        INotificatorService notificator,
        DatabaseContext context,
        UserManager<IdentityBaseDto> userManager
        ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] SignupRequest request)
        {
            
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] PetHelpLoginRequest request)
        {

            return Ok();
        }
    }
}
