using Microsoft.AspNetCore.Mvc;
using ProjetoProva.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Folha de Pagamento");

app.MapPost("api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
{
  ctx.TabelaFuncionarios.Add(funcionario);
  ctx.SaveChanges();
  return Results.Created("", funcionario);
});

app.MapGet("api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{
  return Results.Ok(ctx.TabelaFuncionarios.ToList());
});

app.MapPost("api/folha/cadastrar", ([FromBody] Folha folha, [FromServices] AppDataContext ctx) =>
{
  Funcionario? funcionario = ctx.TabelaFuncionarios.Find(folha.FuncionarioId);

  if (funcionario == null)
  {
    return Results.NotFound();
  }

  folha.SalarioBruto = folha.Valor * folha.Quantidade;

  double aliqua, PD, SB = folha.SalarioBruto,INSS;

  //Calculo IRRF imposto de renda
  if (SB <= 1903.98)
  {
    aliqua = 0;
    PD = 0;
  }
  else if (SB >= 1903.99 && SB <= 2826.65)
  {
    aliqua = 0.075;
    PD = 142.80;
  }
  else if (SB >= 2826.66 && SB <= 3751.05)
  {
    aliqua = 0.15;
    PD = 354.80;
  }
  else if (SB >= 3751.06 && SB <= 4664.68)
  {
    aliqua = 0.225;
    PD = 636.13;
  }
  else
  {
    aliqua = 0.275;
    PD = 869.36;
  }
  folha.Impostolrrf = double.Round((SB * aliqua) - PD, 2, MidpointRounding.AwayFromZero);

  //Calculo INSS
  if (SB <= 1693.72)
  {
    INSS = SB * 0.08;
  }
  else if (SB >= 1693.73 && SB <= 2822.90)
  {
    INSS = SB * 0.09;
  }
  else if (SB >= 2822.91 && SB <= 5645.80)
  {
    INSS = SB * 0.11;
  }
  else
  {
    INSS = 621.03;
  }
  folha.Impostolnss = double.Round(INSS, 2, MidpointRounding.AwayFromZero);
  //Calculo FGTS
  folha.ImpostoFgts = double.Round(SB * 0.08, 2, MidpointRounding.AwayFromZero);

  //Calculo Salario Liquido
  folha.SalarioLiquido = SB - double.Round(folha.Impostolnss - folha.Impostolrrf, 2, MidpointRounding.AwayFromZero);

  folha.Funcionario = funcionario;
  ctx.TabelaFolhas.Add(folha);
  ctx.SaveChanges();

  return Results.Created("", folha);

});

app.MapGet("api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
  List<Folha> folhas = ctx.TabelaFolhas.ToList();
  
  foreach(Folha folha in folhas)
  {
      folha.Funcionario = ctx.TabelaFuncionarios.Find(folha.FuncionarioId);
  }

  return Results.Ok(folhas);
});

app.MapGet("api/folha/buscar/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDataContext ctx) =>
{
  Funcionario? funcionario = ctx.TabelaFuncionarios.FirstOrDefault(f => f.CPF == cpf);

  if(funcionario == null)
  {
    return Results.NotFound("Funcionário não encontrado");
  }

  Folha? folha = ctx.TabelaFolhas.Where(F => F.FuncionarioId == funcionario.funcionarioId).Where(a => a.Ano == ano).FirstOrDefault(m => m.Mes == mes);

  if(folha == null)
  {
    return Results.NotFound("Folha não encontrada");
  }

  return Results.Ok(folha);
});

app.Run();
