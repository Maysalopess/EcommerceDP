namespace TrabalhoDesignPatterns.WebAPI.Services.Strategies;

public interface IFrete
{
    double CalcularFrete(double valorPedido);
}