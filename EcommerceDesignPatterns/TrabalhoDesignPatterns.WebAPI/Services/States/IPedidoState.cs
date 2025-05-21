namespace TrabalhoDesignPatterns.WebAPI.Services.States
{

    public interface IPedidoState
    {
        IPedidoState SucessoAoPagar();
        IPedidoState DespacharPedido();
        IPedidoState CancelarPedido();
    }
}
