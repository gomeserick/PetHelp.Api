using Microsoft.AspNetCore.Identity;
using PetHelp.Application.Security;
using PetHelp.Services.Seeders.Interfaces;
using System.Data;

namespace PetHelp.Services.Seeders
{
    public class RoleSeeder(RoleManager<IdentityRole<int>> roleManager) : ISeeder
    {
        public async Task Seed()
        {
            // Create roles
            if (!await roleManager.RoleExistsAsync(PetHelpRoles.SysAdm))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(PetHelpRoles.SysAdm));
            }
            if (!await roleManager.RoleExistsAsync(PetHelpRoles.Employee))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(PetHelpRoles.Employee));
            }
            if (!await roleManager.RoleExistsAsync(PetHelpRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(PetHelpRoles.User));
            }
        }
    }
}
