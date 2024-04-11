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

            builder.HasIndex(e => e.CPF).IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.CPF)
                .IsRequired();

            builder.Property(e => e.RG)
                .IsRequired();

            builder.Property(e => e.Notification);

            builder.Property(e => e.Address);

            builder.HasOne(e => e.User)
                .WithOne(e => e.Client)
                .HasForeignKey<ClientDto>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
