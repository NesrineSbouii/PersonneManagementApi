using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prenom",
                table: "Personnes",
                newName: "LastNaame");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Personnes",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "DateNaissance",
                table: "Personnes",
                newName: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastNaame",
                table: "Personnes",
                newName: "Prenom");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Personnes",
                newName: "Nom");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Personnes",
                newName: "DateNaissance");
        }
    }
}
