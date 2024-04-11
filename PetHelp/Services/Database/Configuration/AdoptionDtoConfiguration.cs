using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class AdoptionDtoConfiguration: IEntityTypeConfiguration<AdoptionDto>
    {
        public void Configure(EntityTypeBuilder<AdoptionDto> builder)
        {
            builder.ToTable("Adoptions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Observation)
                .IsRequired();

            builder
                .HasMany(e => e.ClientAnimals)
                .WithOne(e => e.Adoption)
                .HasForeignKey(e => e.AdoptionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(e => e.Employee)
                .WithMany(e => e.Adoption)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
