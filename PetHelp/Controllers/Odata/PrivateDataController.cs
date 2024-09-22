using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Application.Contracts.Responses;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;

namespace PetHelp.Controllers.Odata
{
    [Authorize("EndUser")]
    public class PrivateDataController(
        DatabaseContext dbContext, 
        UserManager<IdentityBaseDto> manager,
        IContext context,
        IMapper mapper) : ODataController
    {

        [EnableQuery]
        [HttpGet("User/Animal")]
        public IActionResult GetUserAnimals()
        {
            return Ok(dbContext.Animals.Where(e => e.UserId == context.UserId));
        }

        [EnableQuery]
        [HttpGet("User/Adoption")]
        public IActionResult GetAdoptions()
        {
            return Ok(dbContext.Adoptions);
        }

        [EnableQuery]
        [HttpGet("User/Apointments")]
        public IActionResult GetApointments()
        {
            return Ok(dbContext.Apointments);
        }

        [EnableQuery]
        [HttpGet("User/Clinic")]
        public IActionResult GetClinics()
        {
            return Ok(dbContext.Clinics);
        }

        [EnableQuery]
        [HttpGet("User/AdoptionHeader")]
        public IActionResult GetAdoptionHeaders()
        {
            return Ok(dbContext.Adoptions);
        }

        [EnableQuery]
        [HttpGet("User/Apointment")]
        public IActionResult GetApointmentHeaders()
        {
            return Ok(dbContext.Apointments);
        }

        [EnableQuery]
        [HttpGet("User/ApointmentResults")]
        public IActionResult GetApointmentDetails()
        {
            return Ok(dbContext.ApointmentResults);
        }

        [EnableQuery]
        [HttpGet("User/Medication")]
        public IActionResult GetMedications()
        {
            return Ok(dbContext.Medication);
        }

        [EnableQuery]
        [HttpGet("User/Schedule")]
        public IActionResult GetSchedules()
        {
            return Ok(dbContext.Schedules);
        }

        [EnableQuery]
        [HttpGet("User/Vaccine")]
        public IActionResult GetVaccines()
        {
            return Ok(dbContext.Vaccines);
        }

        [EnableQuery]
        [HttpGet("User/Watched")]
        public IActionResult GetWatcheds()
        {
            return Ok(dbContext.Watcheds);
        }

        [HttpGet("Info")]
        public async Task<IActionResult> Get()
        {
            var user = await manager.GetUserAsync(User);
            var claims = User.Claims.ToDictionary(k => k.Type, v => v.ValueType);
            user.Employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id);
            user.User = await dbContext.PetHelpUsers.FirstOrDefaultAsync(e => e.UserId == user.Id);
            var userResponse = mapper.Map<UserInfoResponse>(user);
            userResponse.Claims = claims;
            return Ok(userResponse);
        }
    }
}
