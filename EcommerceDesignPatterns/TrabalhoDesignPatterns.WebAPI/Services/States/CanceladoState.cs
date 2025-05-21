namespace TrabalhoDesignPatterns.WebAPI.Services.States
{
    public class CanceladoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            throw new InvalidOperationException("Operação não suportada: o pedido foi cancelado.");
        }

        public IPedidoState DespacharPedido()
        {
            throw new InvalidOperationException("Operação não suportada: o pedido foi cancelado.");
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new InvalidOperationException("Operação não suportada: o pedido foi cancelado.");
        }
    }
}
