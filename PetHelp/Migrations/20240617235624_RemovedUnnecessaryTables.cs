using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnnecessaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionDtoAnimalDto");

            migrationBuilder.DropTable(
                name: "AnimalDtoClientDto");

            migrationBuilder.AddColumn<int>(
                name: "AdoptionId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AdoptionId",
                table: "Animals",
                column: "AdoptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ClientId",
                table: "Animals",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Adoptions_AdoptionId",
                table: "Animals",
                column: "AdoptionId",
                principalTable: "Adoptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Clients_ClientId",
                table: "Animals",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Adoptions_AdoptionId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Clients_ClientId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AdoptionId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ClientId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AdoptionId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Animals");

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

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionDtoAnimalDto_AnimalsId",
                table: "AdoptionDtoAnimalDto",
                column: "AnimalsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalDtoClientDto_ClientsId",
                table: "AnimalDtoClientDto",
                column: "ClientsId");
        }
    }
}
