using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backENDCliente.Migrations
{
    public partial class v04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetodosDePagos",
                columns: table => new
                {
                    idMetodoPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numero = table.Column<int>(type: "int", nullable: false),
                    fechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nombreTitular = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosDePagos", x => x.idMetodoPago);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetodosDePagos");
        }
    }
}
