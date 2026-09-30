namespace LojaPerfumesWPF.Models;

public class Perfume
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Marca { get; set; } = "";
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}
