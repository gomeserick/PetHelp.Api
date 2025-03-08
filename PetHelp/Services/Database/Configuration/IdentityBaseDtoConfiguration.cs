using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;
using PetHelp.Dtos.Identity;

namespace PetHelp.Services.Database.Configuration
{
    public class IdentityBaseDtoConfiguration : IEntityTypeConfiguration<IdentityBaseDto>
    {
        public void Configure(EntityTypeBuilder<IdentityBaseDto> builder)
        {

            builder.HasIndex(e => e.CPF).IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name);

            builder.Property(e => e.CPF);

            builder.Property(e => e.RG);

            builder.Property(e => e.Address);
        }
    }
}
