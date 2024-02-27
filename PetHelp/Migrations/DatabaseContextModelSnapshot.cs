﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetHelp.Services.Database;

#nullable disable

namespace PetHelp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdoptionDtoAnimalDto", b =>
                {
                    b.Property<int>("AdoptionsId")
                        .HasColumnType("int");

                    b.Property<int>("AnimalsId")
                        .HasColumnType("int");

                    b.HasKey("AdoptionsId", "AnimalsId");

                    b.HasIndex("AnimalsId");

                    b.ToTable("AdoptionDtoAnimalDto");
                });

            modelBuilder.Entity("AnimalDtoClientDto", b =>
                {
                    b.Property<int>("AnimalsId")
                        .HasColumnType("int");

                    b.Property<int>("ClientsId")
                        .HasColumnType("int");

                    b.HasKey("AnimalsId", "ClientsId");

                    b.HasIndex("ClientsId");

                    b.ToTable("AnimalDtoClientDto");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Adoptions", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.AnimalDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Temperament")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.ToTable("Animals", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.ClientAnimalDto", b =>
                {
                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("AdoptionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("AnimalId", "ClientId");

                    b.HasIndex("AdoptionId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientAnimals", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.ClientDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Notification")
                        .HasColumnType("bit");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.ClinicDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clinics", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.EmployeeDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RG")
                        .IsUnique();

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.ScheduleDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Schedules", (string)null);
                });

            modelBuilder.Entity("AdoptionDtoAnimalDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AdoptionDto", null)
                        .WithMany()
                        .HasForeignKey("AdoptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.AnimalDto", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalDtoClientDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ClientDto", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.ClientDto", "Client")
                        .WithMany("Adoptions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.EmployeeDto", "Employee")
                        .WithMany("Adoption")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PetHelp.Dtos.AnimalDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.ClinicDto", "Clinic")
                        .WithMany("Animals")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("PetHelp.Dtos.ClientAnimalDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AdoptionDto", "Adoption")
                        .WithMany("ClientAnimals")
                        .HasForeignKey("AdoptionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("ClientAnimals")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ClientDto", "Client")
                        .WithMany("ClientAnimals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Adoption");

                    b.Navigation("Animal");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("PetHelp.Dtos.ScheduleDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("Schedules")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ClientDto", "Client")
                        .WithMany("Schedules")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.EmployeeDto", "Employee")
                        .WithMany("Schedules")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionDto", b =>
                {
                    b.Navigation("ClientAnimals");
                });

            modelBuilder.Entity("PetHelp.Dtos.AnimalDto", b =>
                {
                    b.Navigation("ClientAnimals");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("PetHelp.Dtos.ClientDto", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("ClientAnimals");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("PetHelp.Dtos.ClinicDto", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("PetHelp.Dtos.EmployeeDto", b =>
                {
                    b.Navigation("Adoption");

                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
