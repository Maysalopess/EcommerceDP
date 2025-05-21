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
            throw new InvalidOperationException("Operação não suportada: o pedido ainda não foi pago.");
        }

        public IPedidoState SucessoAoPagar()
        {
            return new PagoState();
        }
    }
}
