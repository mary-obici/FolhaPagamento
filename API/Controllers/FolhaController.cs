using FolhaPagamento.Data;
using FolhaPagamento.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/folha")]
    public class FolhaController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public FolhaController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Folha> Folhas = _ctx.Folhas.ToList();
                return Folhas.Count == 0 ? NotFound() : Ok(Folhas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    [HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] Folha folha)
{
    try
    {
        _ctx.Folhas.Add(folha);
        _ctx.SaveChanges();

        return Created("", new { message = "Folha de pagamento cadastrada com sucesso!", folha });
    }
    catch (Exception)
    {
        return BadRequest(new { message = "Não foi possível cadastrar a folha de pagamento!" });
    }
}


[HttpGet]
[Route("buscar/{id}")]
public IActionResult Buscar([FromRoute] int id)
{
    try
    {
        Folha folhaEncontrada = _ctx.Folhas.FirstOrDefault(x => x.FolhaId == id);

        if (folhaEncontrada == null)
            return NotFound();

        // Calcular o salário bruto
        decimal salarioBruto = folhaEncontrada.Quantidade * folhaEncontrada.Valor;

        // Calcular o imposto de renda
        decimal impostoRenda = CalcularImpostoRenda(salarioBruto);

        // Calcular o INSS
        decimal inss = CalcularINSS(salarioBruto);

        // Calcular o FGTS
        decimal fgts = salarioBruto * 0.08m;

        // Calcular o salário líquido
        decimal salarioLiquido = salarioBruto - impostoRenda - inss;

        // Criar um objeto com os resultados
        var resultado = new
        {
            SalarioBruto = salarioBruto,
            ImpostoRenda = impostoRenda,
            INSS = inss,
            FGTS = fgts,
            SalarioLiquido = salarioLiquido
        };

        return Ok(resultado);
    }
    catch (Exception e)
    {
        return BadRequest();
    }
}

private decimal CalcularImpostoRenda(decimal salarioBruto)
{
    // Tabela de alíquotas progressivas do IRRF em 2023 para o Brasil
    if (salarioBruto <= 1903.98m)
        return 0.0m;
    else if (salarioBruto <= 2826.65m)
        return salarioBruto * 0.075m - 142.80m;
    else if (salarioBruto <= 3751.05m)
        return salarioBruto * 0.15m - 354.80m;
    else if (salarioBruto <= 4664.68m)
        return salarioBruto * 0.225m - 636.13m;
    else
        return salarioBruto * 0.275m - 869.36m;
}

private decimal CalcularINSS(decimal salarioBruto)
{
    // Implementação do cálculo do INSS conforme as faixas de salário
    // ... implemente o cálculo de acordo com a lógica fornecida
    // Exemplo de implementação fictícia:
    if (salarioBruto <= 1693.72m)
        return salarioBruto * 0.08m;
    else if (salarioBruto <= 2822.90m)
        return salarioBruto * 0.09m;
    else if (salarioBruto <= 5645.80m)
        return salarioBruto * 0.11m;
    else
        return 621.03m;
}

    }
}