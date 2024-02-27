using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;

namespace PetHelp.Services.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<AdoptionDto> Adoptions { get; set; }
        public DbSet<AnimalDto> Animals { get; set; }
        public DbSet<ClientAnimalDto> ClientAnimals { get; set; }
        public DbSet<ClientDto> Clients { get; set; }
        public DbSet<ClinicDto> Clinics { get; set; }
        public DbSet<EmployeeDto> Employees { get; set; }
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

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
