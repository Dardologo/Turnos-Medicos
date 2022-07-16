using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turnos.Migrations
{
    public partial class addmgrationmodificaciondidEspecialidadyMedico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medicos",
                newName: "IdMedico");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Especialidades",
                newName: "IdEspecialidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdMedico",
                table: "Medicos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdEspecialidad",
                table: "Especialidades",
                newName: "Id");
        }
    }
}
