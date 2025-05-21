namespace TrabalhoDesignPatterns.WebAPI.Services.States
{
    public class AguardandoPagamentoState : IPedidoState
    {
        public IPedidoState CancelarPedido()
        {
            return new CanceladoState();
        }

        public IPedidoState DespacharPedido()
        {
            throw new InvalidOperationException("Opera��o n�o suportada: o pedido ainda n�o foi pago.");
        }

        public IPedidoState SucessoAoPagar()
        {
            return new PagoState();
        }
    }
}
