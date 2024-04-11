using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;

namespace PetHelp.Services.Database
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<IdentityBaseDto, IdentityRole<int>, int>(options)
    {
        public DbSet<AdoptionDto> Adoptions { get; set; }
        public DbSet<AnimalDto> Animals { get; set; }
        public DbSet<ClientAnimalDto> ClientAnimals { get; set; }
        public DbSet<ClientDto> Clients { get; set; }
        public DbSet<ClinicDto> Clinics { get; set; }
        public DbSet<EmployeeDto> Employees { get; set; }
        public DbSet<MessageDto> Messages { get; set; }
        public DbSet<ScheduleDto> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            base.OnModelCreating(model);
        }
    }
}
