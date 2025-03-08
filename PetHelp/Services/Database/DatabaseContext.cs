using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetHelp.Dtos;
using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;
using PetHelp.Services.Context.Interfaces;

namespace PetHelp.Services.Database
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options, IContext context) : IdentityDbContext<IdentityBaseDto, IdentityRole<int>, int>(options)
    {
        public DbSet<AdoptionHeaderDto> Adoptions { get; set; }
        public DbSet<AdoptionDetailDto> AdoptionDetails { get; set; }
        public DbSet<AnimalDto> Animals { get; set; }
        public DbSet<ApointmentHeaderDto> Apointments { get; set; }
        public DbSet<ApointmentDetailDto> ApointmentDetails { get; set; }
        public DbSet<ApointmentResultDto> ApointmentResults { get; set; }
        public DbSet<ClinicDto> Clinics { get; set; }
        public DbSet<EmployeeDto> Employees { get; set; }
        public DbSet<MedicationDto> Medication { get; set; }
        public DbSet<ScheduleDto> Schedules { get; set; }
        public DbSet<UserDto> PetHelpUsers { get; set; }
        public DbSet<VaccineDto> Vaccines { get; set; }
        public DbSet<WatchedDto> Watcheds { get; set; }
        public DbSet<AddressDto> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.LogTo(e =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            });
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<PrivateDataDto>().UseTpcMappingStrategy();
            model.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            SetPrivateDataGlobalQueryFilter(model);
            base.OnModelCreating(model);
        }

        private void SetPrivateDataGlobalQueryFilter(ModelBuilder model)
        {
            var assembly = typeof(DatabaseContext).Assembly;
            var types = assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(typeof(PrivateDataDto)));
            foreach (var type in types)
            {
                model.Entity<PrivateDataDto>().HasQueryFilter(e => EF.Property<int>(e, nameof(e.UserId)) == context.UserId);
            }

            model.Entity<AnimalDto>().HasQueryFilter(e => context.IsEmployee || e.UserId == null || e.UserId == context.UserId);
        }
    }
}
