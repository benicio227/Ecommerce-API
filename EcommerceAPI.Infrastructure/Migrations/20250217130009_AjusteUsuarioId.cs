using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class AjusteUsuarioId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Configura a coluna 'Id' como auto-incremento no MySQL
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValueSql: "AUTO_INCREMENT", // MySQL syntax para auto-incremento
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Caso a migração seja revertida, podemos voltar à configuração original
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
