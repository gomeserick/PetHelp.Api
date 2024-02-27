using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelp.Dtos;
using System.ComponentModel;

namespace PetHelp.Services.Database.Configuration
{
    public class ClinicDtoConfiguration: IEntityTypeConfiguration<ClinicDto>
    {
        public void Configure(EntityTypeBuilder<ClinicDto> builder)
        {
            builder.ToTable("Clinics");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.License)
                .IsRequired();

            builder.Property(e => e.Address)
                .IsRequired();

            builder.Property(e => e.Cnpj)
                .IsRequired();
        }
    }
}
