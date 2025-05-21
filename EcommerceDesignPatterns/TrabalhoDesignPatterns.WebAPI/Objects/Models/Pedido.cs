using TrabalhoDesignPatterns.WebAPI.Objects.Enums;

namespace TrabalhoDesignPatterns.WebAPI.Objects.Models;

public class Pedido
{
    public int Id { get; set; }
    public double Subtotal { get; set; }
    public double ValorFrete { get; set; }
    public EstadoPedido EstadoAtual { get; set; }
    public TipoFrete TipoFrete { get; set; }
    public DateTime Data { get; set; }
    public string EnderecoEntrega { get; set; }

    public Pedido() { }

    public Pedido(int id, double subtotal, double valorFrete, TipoFrete tipoFrete, DateTime data, string enderecoEntrega)
    {
        Id = id;
        Subtotal = subtotal;
        ValorFrete = valorFrete;
        EstadoAtual = EstadoPedido.AguardandoPagamento;
        TipoFrete = tipoFrete;
        Data = data;
        EnderecoEntrega = enderecoEntrega;
    }
}
