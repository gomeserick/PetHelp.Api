using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetHelp.Dtos;
using PetHelp.Dtos.Base;
using PetHelp.Dtos.Identity;

namespace PetHelp.Services.Database
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<IdentityBaseDto, IdentityRole<int>, int>(options)
    {
        public DbSet<AdoptionHeaderDto> Adoptions { get; set; }
        public DbSet<AdoptionDetailDto> AdoptionDetails { get; set; }
        public DbSet<AnimalDto> Animals { get; set; }
        public DbSet<ApointmentHeaderDto> Apointments { get; set; }
        public DbSet<ApointmentResultDto> ApointmentResults { get; set; }
        public DbSet<ClinicDto> Clinics { get; set; }
        public DbSet<EmployeeDto> Employees { get; set; }
        public DbSet<MedicationDto> Medication { get; set; }
        public DbSet<ScheduleDto> Schedules { get; set; }
        public DbSet<UserDto> PetHelpUsers { get; set; }
        public DbSet<VaccineDto> Vaccines { get; set; }
        public DbSet<WatchedDto> Watcheds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(e =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(e);
                Console.ForegroundColor = ConsoleColor.White;
            });
            //optionsBuilder.ConfigureWarnings(wa => wa.Ignore(RelationalEventId.ForeignKeyPropertiesMappedToUnrelatedTables));
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
                model.Entity<PrivateDataDto>().HasQueryFilter(e => EF.Property<bool>(e, nameof(e.UserId)) == false);
            }
        }
    }
}
