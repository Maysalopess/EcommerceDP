namespace TrabalhoDesignPatterns.WebAPI.Services.States
{
    public class PagoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            return new CanceladoState();
        }

        public IPedidoState DespacharPedido()
        {
            return new EnviadoState();
        }

        public IPedidoState SucessoAoPagar()
        {
            throw new InvalidOperationException("Opera��o n�o suportada: o pedido j� foi pago.");
        }
    }
}
