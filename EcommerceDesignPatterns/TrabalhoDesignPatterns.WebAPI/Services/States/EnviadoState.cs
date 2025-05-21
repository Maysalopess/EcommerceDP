namespace TrabalhoDesignPatterns.WebAPI.Services.States
{
    public class EnviadoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            throw new InvalidOperationException("Opera��o n�o suportada: o pedido j� foi enviado.");
        }

        public IPedidoState DespacharPedido()
        {
            throw new InvalidOperationException("Opera��o n�o suportada: o pedido j� foi enviado.");
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new InvalidOperationException("Opera��o n�o suportada: o pedido j� foi enviado.");
        }
    }
}
