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
        }
    }
}
