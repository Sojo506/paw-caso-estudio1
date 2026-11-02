using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.Migrations
{
    /// <inheritdoc />
    public partial class ModuloPartidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoLocalId = table.Column<int>(type: "int", nullable: false),
                    EquipoVisitanteId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GolesLocal = table.Column<int>(type: "int", nullable: true),
                    GolesVisitante = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidos_Equipos_EquipoLocalId",
                        column: x => x.EquipoLocalId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidos_Equipos_EquipoVisitanteId",
                        column: x => x.EquipoVisitanteId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoLocalId",
                table: "Partidos",
                column: "EquipoLocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_EquipoVisitanteId",
                table: "Partidos",
                column: "EquipoVisitanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partidos");
        }
    }
}
