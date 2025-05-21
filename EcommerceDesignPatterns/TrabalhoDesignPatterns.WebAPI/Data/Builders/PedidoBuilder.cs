using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TrabalhoDesignPatterns.WebAPI.Objects.Models;

namespace TrabalhoDesignPatterns.WebAPI.Data.Builders
{
    public static class PedidoBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Pedido>();

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Subtotal).IsRequired();
            entity.Property(p => p.ValorFrete).IsRequired();
            entity.Property(p => p.EstadoAtual).IsRequired();
            entity.Property(p => p.TipoFrete).IsRequired();
            entity.Property(p => p.Data).IsRequired();
            entity.Property(p => p.EnderecoEntrega)
                  .HasMaxLength(255)
                  .IsRequired();
        }
    }
}
