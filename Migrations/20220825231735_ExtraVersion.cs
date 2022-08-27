using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutosCDK.Migrations
{
    public partial class ExtraVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraCDK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VersionCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    versionNombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    potencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    precioBase = table.Column<float>(type: "real", nullable: false),
                    combustible = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModeloCDKId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionCDK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VersionCDK_ModelosCDK_ModeloCDKId",
                        column: x => x.ModeloCDKId,
                        principalTable: "ModelosCDK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "versionCDK_ExtraCDK",
                columns: table => new
                {
                    VersionCDKId = table.Column<int>(type: "int", nullable: false),
                    ExtraCDKId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_versionCDK_ExtraCDK", x => new { x.VersionCDKId, x.ExtraCDKId });
                    table.ForeignKey(
                        name: "FK_versionCDK_ExtraCDK_ExtraCDK_ExtraCDKId",
                        column: x => x.ExtraCDKId,
                        principalTable: "ExtraCDK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_versionCDK_ExtraCDK_VersionCDK_VersionCDKId",
                        column: x => x.VersionCDKId,
                        principalTable: "VersionCDK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_versionCDK_ExtraCDK_ExtraCDKId",
                table: "versionCDK_ExtraCDK",
                column: "ExtraCDKId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "versionCDK_ExtraCDK");

            migrationBuilder.DropTable(
                name: "ExtraCDK");

            migrationBuilder.DropTable(
                name: "VersionCDK");
        }
    }
}
