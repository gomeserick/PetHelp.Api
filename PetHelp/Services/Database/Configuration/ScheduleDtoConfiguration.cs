using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class ScheduleDtoConfiguration: IEntityTypeConfiguration<ScheduleDto>
    {
        public void Configure(EntityTypeBuilder<ScheduleDto> builder)
        {
            builder.ToTable("Schedules");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Duration)
                .IsRequired();

            builder.HasOne(e => e.Client)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Animal)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.AnimalId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
