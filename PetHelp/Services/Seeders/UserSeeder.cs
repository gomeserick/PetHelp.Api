using Microsoft.AspNetCore.Identity;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Seeders.Interfaces;

namespace PetHelp.Services.Seeders
{
    public class UserSeeder(UserManager<IdentityBaseDto> userManager) : ISeeder
    {
        public async Task Seed()
        {
            var email = "pethelp@gmail.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if(!await userManager.IsInRoleAsync(user, "admin"))
                    await userManager.AddToRoleAsync(user, "admin");
                return;
            }

            await userManager.CreateAsync(new IdentityBaseDto
            {
                UserName = email,
                Email = email
            }, "admin123");

            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(email), "admin");
        }
    }
}
