using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backENDCliente.Migrations
{
    public partial class v043 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MetodosDePagos",
                table: "MetodosDePagos");

            migrationBuilder.RenameTable(
                name: "MetodosDePagos",
                newName: "MetodosPagos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetodosPagos",
                table: "MetodosPagos",
                column: "idMetodoPago");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MetodosPagos",
                table: "MetodosPagos");

            migrationBuilder.RenameTable(
                name: "MetodosPagos",
                newName: "MetodosDePagos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetodosDePagos",
                table: "MetodosDePagos",
                column: "idMetodoPago");
        }
    }
}
