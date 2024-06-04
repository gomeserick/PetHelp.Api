using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class ClientDtoConfiguration: IEntityTypeConfiguration<ClientDto>
    {
        public void Configure(EntityTypeBuilder<ClientDto> builder)
        {
            builder.ToTable("Clients");

            builder.HasOne(e => e.User)
                .WithOne(e => e.Client)
                .HasForeignKey<ClientDto>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
