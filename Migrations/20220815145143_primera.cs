using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiAutosCDK.Migrations
{
    public partial class primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcasCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marca = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasCDK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModelosCDK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MarcaCDKId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosCDK", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelosCDK_MarcasCDK_MarcaCDKId",
                        column: x => x.MarcaCDKId,
                        principalTable: "MarcasCDK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelosCDK_MarcaCDKId",
                table: "ModelosCDK",
                column: "MarcaCDKId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelosCDK");

            migrationBuilder.DropTable(
                name: "MarcasCDK");
        }
    }
}
