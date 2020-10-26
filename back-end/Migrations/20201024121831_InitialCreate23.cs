using Microsoft.EntityFrameworkCore.Migrations;

namespace agendamento.Migrations
{
    public partial class InitialCreate23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFornecedor",
                table: "Agendamento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdFornecedor",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
