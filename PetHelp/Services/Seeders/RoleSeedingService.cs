using Microsoft.AspNetCore.Identity;
using PetHelp.Services.Seeders.Interfaces;

namespace PetHelp.Services.Seeders
{
    public class RoleSeedingService(RoleManager<IdentityRole<int>> roleManager) : ISeeder
    {
        public async Task Seed()
        {
            List<string> roles = ["admin", "employee", "client"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new(role));
            }
        }
    }
}
