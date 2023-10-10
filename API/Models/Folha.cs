using FolhaPagamento.Models;

public class Folha
{
    public int FolhaId { get; set; }
    public int Valor { get; set;}
    public int Quantidade { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int EmployeeId { get; set; }  // Alterado para int para armazenar o ID do funcionário
    public Employee Employee { get; set; } // Adicionada propriedade para representar a associação com Employee
}