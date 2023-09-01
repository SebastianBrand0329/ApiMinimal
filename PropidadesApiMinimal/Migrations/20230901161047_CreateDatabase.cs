using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropidadesApiMinimal.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "propiedads",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_propiedads", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "propiedads",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Test 1", new DateTime(2023, 9, 1, 11, 10, 47, 418, DateTimeKind.Local).AddTicks(757), "Casa las Palmas", "Cartagena" },
                    { 2, true, "Test 2", new DateTime(2023, 9, 1, 11, 10, 47, 418, DateTimeKind.Local).AddTicks(777), "Casa laureles", "Medellin" },
                    { 3, true, "Test 2", new DateTime(2023, 9, 1, 11, 10, 47, 418, DateTimeKind.Local).AddTicks(779), "Casa los Colores", "Bogotá" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "propiedads");
        }
    }
}
