namespace ProjetoProva.Models;

public class Folha
{
  public int folhaId { get; set; }
  public double Valor { get; set; }
  public int Quantidade { get; set; }
  public int Mes { get; set; }
  public int Ano { get; set; }
  public double SalarioBruto { get; set; }
  public double Impostolrrf { get; set; }
  public double Impostolnss { get; set; }
  public double ImpostoFgts { get; set; }
  public double SalarioLiquido { get; set; }
  public Funcionario? Funcionario { get; set; }
  public int FuncionarioId { get; set; }

}
