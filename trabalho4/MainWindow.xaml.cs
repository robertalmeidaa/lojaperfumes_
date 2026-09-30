using System.Windows;
using LojaPerfumesWPF.Data;
using LojaPerfumesWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaPerfumesWPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CarregarPerfumes();
    }

    private void CarregarPerfumes()
    {
        try
        {
            using var db = new AppDbContext();
            TabelaPerfumes.ItemsSource = db.Perfumes.AsNoTracking().ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar os perfumes: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Adicionar_Click(object sender, RoutedEventArgs e)
    {
        var tela = new PerfumeWindow();
        if (tela.ShowDialog() == true)
        {
            try
            {
                using var db = new AppDbContext();
                db.Perfumes.Add(tela.Perfume);
                db.SaveChanges();
                CarregarPerfumes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar o perfume: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void Editar_Click(object sender, RoutedEventArgs e)
    {
        if (TabelaPerfumes.SelectedItem is not Perfume selecionado)
        {
            MessageBox.Show("Selecione um perfume.", "Nenhum item selecionado", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var tela = new PerfumeWindow(selecionado);
        if (tela.ShowDialog() == true)
        {
            try
            {
                using var db = new AppDbContext();
                var perfume = db.Perfumes.Find(selecionado.Id);

                if (perfume == null)
                {
                    MessageBox.Show("O perfume selecionado não existe mais no banco de dados.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    CarregarPerfumes();
                    return;
                }

                perfume.Nome = tela.Perfume.Nome;
                perfume.Marca = tela.Perfume.Marca;
                perfume.Preco = tela.Perfume.Preco;
                perfume.Estoque = tela.Perfume.Estoque;
                db.SaveChanges();

                CarregarPerfumes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao editar o perfume: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void Excluir_Click(object sender, RoutedEventArgs e)
    {
        if (TabelaPerfumes.SelectedItem is not Perfume selecionado)
        {
            MessageBox.Show("Selecione um perfume.", "Nenhum item selecionado", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var resposta = MessageBox.Show(
            $"Deseja excluir o perfume {selecionado.Nome}?",
            "Excluir",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (resposta != MessageBoxResult.Yes)
            return;

        try
        {
            using var db = new AppDbContext();
            var perfume = db.Perfumes.Find(selecionado.Id);

            if (perfume != null)
            {
                db.Perfumes.Remove(perfume);
                db.SaveChanges();
            }

            CarregarPerfumes();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao excluir o perfume: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Buscar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var termo = CaixaBusca.Text.Trim();

            using var db = new AppDbContext();
            TabelaPerfumes.ItemsSource = db.Perfumes
                .AsNoTracking()
                .Where(p => p.Nome.Contains(termo))
                .ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao buscar perfumes: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LimparBusca_Click(object sender, RoutedEventArgs e)
    {
        CaixaBusca.Clear();
        CarregarPerfumes();
    }
}
