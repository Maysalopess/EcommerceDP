using Microsoft.EntityFrameworkCore;
using TrabalhoDesignPatterns.WebAPI.Data.Builders;
using TrabalhoDesignPatterns.WebAPI.Objects.Models;

namespace TrabalhoDesignPatterns.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        PedidoBuilder.Build(modelBuilder);
    }
}