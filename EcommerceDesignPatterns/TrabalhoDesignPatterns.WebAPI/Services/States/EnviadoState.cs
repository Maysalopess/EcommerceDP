namespace TrabalhoDesignPatterns.WebAPI.Services.States
{
    public class EnviadoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            throw new InvalidOperationException("Operação não suportada: o pedido já foi enviado.");
        }

        public IPedidoState DespacharPedido()
        {
            throw new InvalidOperationException("Operação não suportada: o pedido já foi enviado.");
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new InvalidOperationException("Operação não suportada: o pedido já foi enviado.");
        }
    }
}
