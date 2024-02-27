using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class ClientAnimalDtoConfiguration: IEntityTypeConfiguration<ClientAnimalDto>
    {
        public void Configure(EntityTypeBuilder<ClientAnimalDto> builder)
        {
            builder.ToTable("ClientAnimals");

            builder.HasKey(e => new { e.AnimalId, e.ClientId });

            builder.Property(e => e.AdoptionId)
                .IsRequired(true);

            builder.HasOne(e => e.Client)
                .WithMany(e => e.ClientAnimals)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Animal)
                .WithMany(e => e.ClientAnimals)
                .HasForeignKey(e => e.AnimalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Adoption)
                .WithMany(e => e.ClientAnimals)
                .HasForeignKey(e => e.AdoptionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
