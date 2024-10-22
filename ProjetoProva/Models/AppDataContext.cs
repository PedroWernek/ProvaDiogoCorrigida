using Microsoft.EntityFrameworkCore;

namespace ProjetoProva.Models;

public class AppDataContext : DbContext
{
  public DbSet<Funcionario> TabelaFuncionarios { get; set; }
  public DbSet<Folha> TabelaFolhas { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=Banco.db");
  }
}
