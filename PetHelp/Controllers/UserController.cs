using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Responses;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;

namespace PetHelp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Default")]
    public class UserController(
        DatabaseContext context,
        IMapper mapper,
        UserManager<IdentityBaseDto> manager) : ControllerBase
    {
        [HttpGet("Info")]
        public async Task<IActionResult> Get()
        {
            var user = await manager.GetUserAsync(User);
            var claims = await manager.GetRolesAsync(user);
            user.Employee = await context.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id);
            user.Client = await context.Clients.FirstOrDefaultAsync(e => e.UserId == user.Id);
            var userResponse = mapper.Map<UserInfoResponse>(user);
            userResponse = mapper.Map(user, userResponse);
            userResponse.Roles = claims;
            return Ok(userResponse);
        }

        //public async Task<IActionResult> Get(int key)
        //{
        //    var result = await context.Users.Where(e => e.Id == key).ToListAsync();

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}
    }
}
