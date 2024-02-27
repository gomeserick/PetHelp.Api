using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;

namespace PetHelp.Services.Database.Configuration
{
    public class EmployeeDtoConfiguration: IEntityTypeConfiguration<EmployeeDto>
    {
        public void Configure(EntityTypeBuilder<EmployeeDto> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.RG).IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.RG)
                .IsRequired();

            builder.Property(e => e.Image);
        }
    }
}
