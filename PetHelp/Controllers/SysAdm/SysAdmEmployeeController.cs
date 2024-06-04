using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PetHelp.Services.Database;

namespace PetHelp.Controllers.SysAdm
{
    public class SysAdmEmployeeController(DatabaseContext context): ODataController
    {
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(context.Users.Include(e => e.Employee).Where(e => e.Employee != null));
        }
    }
}
