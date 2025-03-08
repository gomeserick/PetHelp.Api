using Microsoft.AspNetCore.Identity;
using PetHelp.Application.Security;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Database;
using PetHelp.Services.Seeders.Interfaces;

namespace PetHelp.Services.Seeders
{
    public class UserSeeder(UserManager<IdentityBaseDto> userManager, DatabaseContext dbContext) : ISeeder
    {
        public async Task Seed()
        {
            await SeedSingle("pethelp@gmail.com", "admin123", PetHelpRoles.SysAdm);
            await SeedSingle("pethelpUser@gmail.com", "user12345", PetHelpRoles.User);

            var employeeId = (await userManager.FindByEmailAsync("pethelp@gmail.com")).Id;
            var userId = (await userManager.FindByEmailAsync("pethelpUser@gmail.com")).Id;

            if (!dbContext.PetHelpUsers.Any(e => e.UserId == userId))
                dbContext.Add<UserDto>(new()
                {
                    Id = userId,
                    UserId = userId,
                });

            if (!dbContext.Employees.Any(e => e.UserId == employeeId))
                dbContext.Add<EmployeeDto>(new()
                {
                    Id = employeeId,
                    UserId = employeeId,
                });

            await dbContext.SaveChangesAsync();
        }

        public async Task SeedSingle(string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (!await userManager.IsInRoleAsync(user, role))
                    await userManager.AddToRoleAsync(user, role);
                return;
            }

            await userManager.CreateAsync(new IdentityBaseDto
            {
                UserName = email,
                Email = email
            }, password);

            user = await userManager.FindByEmailAsync(email);

            await userManager.AddToRoleAsync(user, role);
        }
    }
}
