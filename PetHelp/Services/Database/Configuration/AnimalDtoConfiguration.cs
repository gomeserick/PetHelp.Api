using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class AnimalDtoConfiguration : IEntityTypeConfiguration<AnimalDto>
    {
        public void Configure(EntityTypeBuilder<AnimalDto> builder)
        {
            builder.ToTable("Animals");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Color)
                .IsRequired(false);

            builder.Property(e => e.Species)
                .IsRequired();

            builder.Property(e => e.Gender)
                .IsRequired();

            builder.Property(e => e.Temperament)
                .IsRequired();

            builder.Property(e => e.Image)
                .IsRequired(false);

            builder.Property(e => e.ClinicId);

            builder.HasOne(e => e.Clinic)
                .WithMany(e => e.Animals)
                .HasForeignKey(e => e.ClinicId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
