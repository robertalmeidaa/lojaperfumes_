using System.Windows;
using LojaPerfumesWPF.Models;

namespace LojaPerfumesWPF;

public partial class PerfumeWindow : Window
{
    public Perfume Perfume { get; private set; }

    public PerfumeWindow()
    {
        InitializeComponent();
        Perfume = new Perfume();
    }

    public PerfumeWindow(Perfume perfume)
    {
        InitializeComponent();

        Perfume = new Perfume
        {
            Id = perfume.Id,
            Nome = perfume.Nome,
            Marca = perfume.Marca,
            Preco = perfume.Preco,
            Estoque = perfume.Estoque
        };

        NomeBox.Text = Perfume.Nome;
        MarcaBox.Text = Perfume.Marca;
        PrecoBox.Text = Perfume.Preco.ToString();
        EstoqueBox.Text = Perfume.Estoque.ToString();
    }

    private void Salvar_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeBox.Text) ||
            string.IsNullOrWhiteSpace(MarcaBox.Text) ||
            !decimal.TryParse(PrecoBox.Text, out var preco) ||
            !int.TryParse(EstoqueBox.Text, out var estoque))
        {
            MessageBox.Show("Preencha os campos corretamente.");
            return;
        }

        Perfume.Nome = NomeBox.Text.Trim();
        Perfume.Marca = MarcaBox.Text.Trim();
        Perfume.Preco = preco;
        Perfume.Estoque = estoque;

        DialogResult = true;
    }

    private void Cancelar_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
