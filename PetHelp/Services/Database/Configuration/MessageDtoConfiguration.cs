using Microsoft.EntityFrameworkCore;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class MessageDtoConfiguration: IEntityTypeConfiguration<MessageDto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MessageDto> builder)
        {
            builder.ToTable("Message");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
