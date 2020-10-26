using Microsoft.EntityFrameworkCore.Migrations;

namespace agendamento.Migrations
{
    public partial class InitialCreate24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdFornecedor",
                table: "Agendamento",
                column: "IdFornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Fornecedor_IdFornecedor",
                table: "Agendamento",
                column: "IdFornecedor",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Fornecedor_IdFornecedor",
                table: "Agendamento");

            migrationBuilder.DropIndex(
                name: "IX_Agendamento_IdFornecedor",
                table: "Agendamento");
        }
    }
}
