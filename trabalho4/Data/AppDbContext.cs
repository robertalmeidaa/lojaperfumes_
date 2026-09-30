using LojaPerfumesWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaPerfumesWPF.Data;

public class AppDbContext : DbContext
{
    public DbSet<Perfume> Perfumes => Set<Perfume>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conexao = "Server=localhost;Port=3306;Database=loja_perfumes;User=root;Password=vidaloka1;";
        optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    }
}
