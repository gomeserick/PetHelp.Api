using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;
using System.Reflection.Emit;

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

        }


        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<ClientAnimalDto>()
                .HasOne(e => e.Client)
                .WithMany(e => e.ClientAnimals)
                .OnDelete(DeleteBehavior.NoAction);

            model.Entity<ClientAnimalDto>()
                .HasOne(e => e.Animal)
                .WithMany(e => e.ClientAnimals)
                .OnDelete(DeleteBehavior.NoAction);

            model.Entity<ClientAnimalDto>()
                .HasOne(e => e.Adoption)
                .WithMany(e => e.ClientAnimals)
                .OnDelete(DeleteBehavior.NoAction);

            model.Entity<AnimalDto>()
                .HasMany(e => e.Clients)
                .WithMany(e => e.Animals)
                .UsingEntity<ClientAnimalDto>();

            model.Entity<ClientDto>()
                .HasMany(e => e.Animals)
                .WithMany(e => e.Clients)
                .UsingEntity<ClientAnimalDto>();

            model.Entity<AdoptionDto>()
                .HasMany(e => e.Animals)
                .WithMany(e => e.Adoptions)
                .UsingEntity<ClientAnimalDto>();

            model.Entity<AdoptionDto>()
                .HasOne(e => e.Client)
                .WithMany(e => e.Adoptions)
                .HasForeignKey(e => e.ClientId);

            model.Entity<AdoptionDto>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Adoption)
                .HasForeignKey(e => e.EmployeeId);

            model.Entity<AdoptionDto>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Adoption)
                .HasForeignKey(e => e.EmployeeId);

            model.Entity<ClinicDto>()
                .HasMany(e => e.Animals)
                .WithOne(e => e.Clinic)
                .HasForeignKey(e => e.ClinicId);

            base.OnModelCreating(model);
        }
    }
}
