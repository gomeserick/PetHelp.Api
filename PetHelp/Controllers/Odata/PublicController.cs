using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Services.Context.Interfaces;
using PetHelp.Services.Database;

namespace PetHelp.Controllers.Odata
{

    public class PublicController(DatabaseContext dbContext, IContext context) : ODataController
    {
        [EnableQuery]
        [HttpGet("odata/Animal")]
        public IActionResult GetAnimals()
        {
            return Ok(dbContext.Animals.Where(e => e.UserId == null));
        }

        [EnableQuery]
        [HttpGet("odata/Clinic")]
        public IActionResult GetClinics()
        {
            return Ok(dbContext.Clinics);
        }
    }
}
