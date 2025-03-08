using Microsoft.AspNetCore.Identity;
using PetHelp.Application.Contracts.Enums;
using PetHelp.Application.Security;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Seeders.Interfaces;
using System.Security.Claims;

namespace PetHelp.Services.Seeders
{
    public class ClaimSeeder(RoleManager<IdentityRole<int>> roleManager) : ISeeder
    {
        public async Task Seed()
        {
            // Create claims
            List<Claim> allClaims =
            [
                new Claim(PetHelpClaims.Registered, "true"),
                new Claim(PetHelpClaims.CreateAnimal, "true"),
                new Claim(PetHelpClaims.UpdateAnimal, "true"),
                new Claim(PetHelpClaims.CreateVaccine, "true"),
                new Claim(PetHelpClaims.UpdateVaccine, "true"),
                new Claim(PetHelpClaims.CreateMedication, "true"),
                new Claim(PetHelpClaims.UpdateMedication, "true"),
                new Claim(PetHelpClaims.CreateClinic, "true"),
                new Claim(PetHelpClaims.UpdateClinic, "true"),
                new Claim(PetHelpClaims.ConfirmAdoption, "true"),
                new Claim(PetHelpClaims.CreateAdoption, "true"),
                new Claim(PetHelpClaims.AddToWatchList, "true"),
                new Claim(PetHelpClaims.RemoveFromWatchList, "true")
            ];

            await AddClaims(PetHelpRoles.SysAdm, allClaims);

            var employeeClaims = new List<Claim>
            {
                new (PetHelpClaims.Registered, "true"),
                new (PetHelpClaims.CreateAnimal, "true"),
                new (PetHelpClaims.UpdateAnimal, "true"),
                new (PetHelpClaims.CreateVaccine, "true"),
                new (PetHelpClaims.UpdateVaccine, "true"),
                new (PetHelpClaims.CreateMedication, "true"),
                new (PetHelpClaims.UpdateMedication, "true"),
                new (PetHelpClaims.CreateClinic, "true"),
                new (PetHelpClaims.UpdateClinic, "true"),
                new (PetHelpClaims.ConfirmAdoption, "true")
            };

            await AddClaims(PetHelpRoles.Employee, employeeClaims);

            List<Claim> userClaims = 
            [
                new (PetHelpClaims.Registered, "true"),
                new (PetHelpClaims.UpdateAnimal, "true"),
                new (PetHelpClaims.CreateVaccine, "true"),
                new (PetHelpClaims.UpdateVaccine, "true"),
                new (PetHelpClaims.CreateMedication, "true"),
                new (PetHelpClaims.UpdateMedication, "true"),
                new (PetHelpClaims.CreateAdoption, "true"),
                new (PetHelpClaims.AddToWatchList, "true"),
                new (PetHelpClaims.RemoveFromWatchList, "true")
            ];

            await AddClaims(PetHelpRoles.User, userClaims);
        }

        private async Task AddClaims(string roleName, List<Claim> claims)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            var cl = await roleManager.GetClaimsAsync(role);
            var insertClaims = claims
                .Where(e => !cl.Select(e => e.Type).Contains(e.Type))
                .ToList();
            foreach(var claim in insertClaims)
            {
                await roleManager.AddClaimAsync(role, claim);
            }
        }
    }
}
