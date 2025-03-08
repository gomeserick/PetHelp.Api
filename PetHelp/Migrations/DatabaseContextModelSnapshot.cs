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

            modelBuilder.HasSequence("PrivateDataDtoSequence");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.AddressDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("PetHelp.Dtos.AnimalDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Alive")
                        .HasColumnType("bit");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("Breed")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Castrated")
                        .HasColumnType("bit");

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Species")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Temperament")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("UserId");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("PetHelp.Dtos.Base.PrivateDataDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [PrivateDataDtoSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("PetHelp.Dtos.ClinicDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Clinic");
                });

            modelBuilder.Entity("PetHelp.Dtos.EmployeeDto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("PetHelp.Dtos.Identity.IdentityBaseDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("NotificationEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RegistrationFlag")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique()
                        .HasFilter("[CPF] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("PetHelp.Dtos.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionDetailDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<int>("AdoptionHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AdoptionHeaderId");

                    b.HasIndex("AnimalId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionDetail");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionHeaderDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("UserId");

                    b.ToTable("AdoptionHeader");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentDetailDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int?>("ApointmentHeaderDtoId")
                        .HasColumnType("int");

                    b.Property<int>("ApointmentHeaderId")
                        .HasColumnType("int");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ApointmentHeaderDtoId");

                    b.HasIndex("UserId");

                    b.ToTable("ApointmentDetails");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentHeaderDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Type")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasIndex("ClinicId");

                    b.HasIndex("UserId");

                    b.ToTable("Apointment");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentResultDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<int?>("AnimalDtoId")
                        .HasColumnType("int");

                    b.Property<int>("ApointmentHeaderId")
                        .HasColumnType("int");

                    b.Property<int?>("ClinicDtoId")
                        .HasColumnType("int");

                    b.Property<string>("Observations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasIndex("AnimalDtoId");

                    b.HasIndex("ApointmentHeaderId");

                    b.HasIndex("ClinicDtoId");

                    b.HasIndex("UserId");

                    b.ToTable("ApointmentResult");
                });

            modelBuilder.Entity("PetHelp.Dtos.MedicationDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Dose")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<string>("DoseUnitOfMeasurement")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("Frequency")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ClinicId");

                    b.HasIndex("UserId");

                    b.ToTable("Medication");
                });

            modelBuilder.Entity("PetHelp.Dtos.ScheduleDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasIndex("AnimalId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("UserId");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("PetHelp.Dtos.VaccineDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int>("ClinicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTaken")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AnimalId");

                    b.HasIndex("ClinicId");

                    b.HasIndex("UserId");

                    b.ToTable("Vaccine");
                });

            modelBuilder.Entity("PetHelp.Dtos.WatchedDto", b =>
                {
                    b.HasBaseType("PetHelp.Dtos.Base.PrivateDataDto");

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.HasIndex("AnimalId");

                    b.HasIndex("UserId");

                    b.ToTable("Watched");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("PetHelp.Dtos.Identity.IdentityBaseDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("PetHelp.Dtos.Identity.IdentityBaseDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.Identity.IdentityBaseDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("PetHelp.Dtos.Identity.IdentityBaseDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PetHelp.Dtos.AnimalDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.ClinicDto", "Clinic")
                        .WithMany("Animals")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Animals")
                        .HasForeignKey("UserId");

                    b.Navigation("Clinic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.ClinicDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AddressDto", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PetHelp.Dtos.EmployeeDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.Identity.IdentityBaseDto", "User")
                        .WithOne("Employee")
                        .HasForeignKey("PetHelp.Dtos.EmployeeDto", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.UserDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.Identity.IdentityBaseDto", "IdentityUser")
                        .WithOne("User")
                        .HasForeignKey("PetHelp.Dtos.UserDto", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionDetailDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AdoptionHeaderDto", "AdoptionHeader")
                        .WithMany("AdoptionDetails")
                        .HasForeignKey("AdoptionHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany()
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("AdoptionsDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AdoptionHeader");

                    b.Navigation("Animal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionHeaderDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.EmployeeDto", "Employee")
                        .WithMany("Adoptions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Adoptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentDetailDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("Apointments")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ApointmentHeaderDto", null)
                        .WithMany("Details")
                        .HasForeignKey("ApointmentHeaderDtoId");

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentHeaderDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.ClinicDto", "Clinic")
                        .WithMany("Apointments")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Apointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentResultDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", null)
                        .WithMany("ApointmentResults")
                        .HasForeignKey("AnimalDtoId");

                    b.HasOne("PetHelp.Dtos.ApointmentHeaderDto", "ApointmentHeader")
                        .WithMany("Results")
                        .HasForeignKey("ApointmentHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ClinicDto", null)
                        .WithMany("ApointmentsResults")
                        .HasForeignKey("ClinicDtoId");

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Results")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApointmentHeader");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.MedicationDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("Medications")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ClinicDto", "Clinic")
                        .WithMany("Medications")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Medications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Clinic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.ScheduleDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("Schedules")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.EmployeeDto", "Employee")
                        .WithMany("Schedules")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Schedules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Employee");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.VaccineDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("Vaccines")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.ClinicDto", "Clinic")
                        .WithMany("Vaccines")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("Vaccines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Clinic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.WatchedDto", b =>
                {
                    b.HasOne("PetHelp.Dtos.AnimalDto", "Animal")
                        .WithMany("WatchedList")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHelp.Dtos.UserDto", "User")
                        .WithMany("WatchedList")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.AnimalDto", b =>
                {
                    b.Navigation("ApointmentResults");

                    b.Navigation("Apointments");

                    b.Navigation("Medications");

                    b.Navigation("Schedules");

                    b.Navigation("Vaccines");

                    b.Navigation("WatchedList");
                });

            modelBuilder.Entity("PetHelp.Dtos.ClinicDto", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Apointments");

                    b.Navigation("ApointmentsResults");

                    b.Navigation("Medications");

                    b.Navigation("Vaccines");
                });

            modelBuilder.Entity("PetHelp.Dtos.EmployeeDto", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("PetHelp.Dtos.Identity.IdentityBaseDto", b =>
                {
                    b.Navigation("Employee");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetHelp.Dtos.UserDto", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("AdoptionsDetails");

                    b.Navigation("Animals");

                    b.Navigation("Apointments");

                    b.Navigation("Medications");

                    b.Navigation("Results");

                    b.Navigation("Schedules");

                    b.Navigation("Vaccines");

                    b.Navigation("WatchedList");
                });

            modelBuilder.Entity("PetHelp.Dtos.AdoptionHeaderDto", b =>
                {
                    b.Navigation("AdoptionDetails");
                });

            modelBuilder.Entity("PetHelp.Dtos.ApointmentHeaderDto", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
