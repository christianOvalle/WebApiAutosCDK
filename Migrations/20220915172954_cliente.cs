using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutosCDK.Migrations
{
    public partial class cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientesCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesCDK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionesDireccionCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lugar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteCDKId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionesDireccionCDK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionesDireccionCDK_ClientesCDK_ClienteCDKId",
                        column: x => x.ClienteCDKId,
                        principalTable: "ClientesCDK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DireccionClientesCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigo_postal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    calle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numero_casa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteCDKId = table.Column<int>(type: "int", nullable: false),
                    UbicacionDireccionCDKId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionClientesCDK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DireccionClientesCDK_ClientesCDK_ClienteCDKId",
                        column: x => x.ClienteCDKId,
                        principalTable: "ClientesCDK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DireccionClientesCDK_UbicacionesDireccionCDK_UbicacionDireccionCDKId",
                        column: x => x.UbicacionDireccionCDKId,
                        principalTable: "UbicacionesDireccionCDK",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DireccionClientesCDK_ClienteCDKId",
                table: "DireccionClientesCDK",
                column: "ClienteCDKId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionClientesCDK_UbicacionDireccionCDKId",
                table: "DireccionClientesCDK",
                column: "UbicacionDireccionCDKId");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionesDireccionCDK_ClienteCDKId",
                table: "UbicacionesDireccionCDK",
                column: "ClienteCDKId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DireccionClientesCDK");

            migrationBuilder.DropTable(
                name: "UbicacionesDireccionCDK");

            migrationBuilder.DropTable(
                name: "ClientesCDK");
        }
    }
}
