using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoProva.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaFuncionarios",
                columns: table => new
                {
                    funcionarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaFuncionarios", x => x.funcionarioId);
                });

            migrationBuilder.CreateTable(
                name: "TabelaFolhas",
                columns: table => new
                {
                    folhaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Mes = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    SalarioBruto = table.Column<double>(type: "REAL", nullable: false),
                    Impostolrrf = table.Column<double>(type: "REAL", nullable: false),
                    Impostolnss = table.Column<double>(type: "REAL", nullable: false),
                    ImpostoFgts = table.Column<double>(type: "REAL", nullable: false),
                    SalarioLiquido = table.Column<double>(type: "REAL", nullable: false),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaFolhas", x => x.folhaId);
                    table.ForeignKey(
                        name: "FK_TabelaFolhas_TabelaFuncionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "TabelaFuncionarios",
                        principalColumn: "funcionarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaFolhas_FuncionarioId",
                table: "TabelaFolhas",
                column: "FuncionarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaFolhas");

            migrationBuilder.DropTable(
                name: "TabelaFuncionarios");
        }
    }
}
