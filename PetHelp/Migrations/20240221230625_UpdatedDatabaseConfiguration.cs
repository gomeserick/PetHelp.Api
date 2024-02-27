using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatabaseConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clinics_ClinicId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_clientAnimals_Adoptions_AdoptionId",
                table: "clientAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_clientAnimals_Animals_AnimalId",
                table: "clientAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_clientAnimals_Clients_ClientId",
                table: "clientAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientAnimals",
                table: "clientAnimals");

            migrationBuilder.RenameTable(
                name: "clientAnimals",
                newName: "ClientAnimals");

            migrationBuilder.RenameIndex(
                name: "IX_clientAnimals_ClientId",
                table: "ClientAnimals",
                newName: "IX_ClientAnimals_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_clientAnimals_AdoptionId",
                table: "ClientAnimals",
                newName: "IX_ClientAnimals_AdoptionId");

            migrationBuilder.AlterColumn<string>(
                name: "RG",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AdoptionId",
                table: "ClientAnimals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientAnimals",
                table: "ClientAnimals",
                columns: new[] { "AnimalId", "ClientId" });

            migrationBuilder.CreateTable(
                name: "AdoptionDtoAnimalDto",
                columns: table => new
                {
                    AdoptionsId = table.Column<int>(type: "int", nullable: false),
                    AnimalsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionDtoAnimalDto", x => new { x.AdoptionsId, x.AnimalsId });
                    table.ForeignKey(
                        name: "FK_AdoptionDtoAnimalDto_Adoptions_AdoptionsId",
                        column: x => x.AdoptionsId,
                        principalTable: "Adoptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptionDtoAnimalDto_Animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalDtoClientDto",
                columns: table => new
                {
                    AnimalsId = table.Column<int>(type: "int", nullable: false),
                    ClientsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalDtoClientDto", x => new { x.AnimalsId, x.ClientsId });
                    table.ForeignKey(
                        name: "FK_AnimalDtoClientDto_Animals_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalDtoClientDto_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RG",
                table: "Employees",
                column: "RG",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CPF",
                table: "Clients",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionDtoAnimalDto_AnimalsId",
                table: "AdoptionDtoAnimalDto",
                column: "AnimalsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalDtoClientDto_ClientsId",
                table: "AnimalDtoClientDto",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_AnimalId",
                table: "Schedules",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClientId",
                table: "Schedules",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                table: "Schedules",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clinics_ClinicId",
                table: "Animals",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAnimals_Adoptions_AdoptionId",
                table: "ClientAnimals",
                column: "AdoptionId",
                principalTable: "Adoptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAnimals_Animals_AnimalId",
                table: "ClientAnimals",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientAnimals_Clients_ClientId",
                table: "ClientAnimals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clinics_ClinicId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAnimals_Adoptions_AdoptionId",
                table: "ClientAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAnimals_Animals_AnimalId",
                table: "ClientAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientAnimals_Clients_ClientId",
                table: "ClientAnimals");

            migrationBuilder.DropTable(
                name: "AdoptionDtoAnimalDto");

            migrationBuilder.DropTable(
                name: "AnimalDtoClientDto");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RG",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CPF",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientAnimals",
                table: "ClientAnimals");

            migrationBuilder.RenameTable(
                name: "ClientAnimals",
                newName: "clientAnimals");

            migrationBuilder.RenameIndex(
                name: "IX_ClientAnimals_ClientId",
                table: "clientAnimals",
                newName: "IX_clientAnimals_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientAnimals_AdoptionId",
                table: "clientAnimals",
                newName: "IX_clientAnimals_AdoptionId");

            migrationBuilder.AlterColumn<string>(
                name: "RG",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "AdoptionId",
                table: "clientAnimals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientAnimals",
                table: "clientAnimals",
                columns: new[] { "AnimalId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clinics_ClinicId",
                table: "Animals",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_clientAnimals_Adoptions_AdoptionId",
                table: "clientAnimals",
                column: "AdoptionId",
                principalTable: "Adoptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_clientAnimals_Animals_AnimalId",
                table: "clientAnimals",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_clientAnimals_Clients_ClientId",
                table: "clientAnimals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
