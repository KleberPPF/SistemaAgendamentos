using Microsoft.EntityFrameworkCore.Migrations;

namespace agendamento.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Fornecedor_FornecedorId",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_FornecedorId",
                table: "Agendamento");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "IdFornecedor",
                table: "Agendamento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFornecedor",
                table: "Agendamento");

            migrationBuilder.AddColumn<int>(
                name: "FornecedorId",
                table: "Agendamento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_FornecedorId",
                table: "Agendamento",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Fornecedor_FornecedorId",
                table: "Agendamento",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
