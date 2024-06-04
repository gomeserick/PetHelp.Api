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

            builder.HasOne(e => e.User)
                .WithOne(e => e.Employee)
                .HasForeignKey<EmployeeDto>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
