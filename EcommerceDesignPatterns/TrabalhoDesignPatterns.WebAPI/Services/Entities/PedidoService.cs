using TrabalhoDesignPatterns.WebAPI.Data.Interfaces;
using TrabalhoDesignPatterns.WebAPI.Objects.DTOs;
using TrabalhoDesignPatterns.WebAPI.Objects.Enums;
using TrabalhoDesignPatterns.WebAPI.Objects.Models;
using TrabalhoDesignPatterns.WebAPI.Services.Interfaces;
using TrabalhoDesignPatterns.WebAPI.Services.States;
using TrabalhoDesignPatterns.WebAPI.Services.Strategies;

namespace TrabalhoDesignPatterns.WebAPI.Services.Entities;


public class PedidoService : IPedidoService

{
    private readonly IPedidoRepository _repository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _repository = pedidoRepository;
    }

    public async Task<IEnumerable<PedidoDTO>> ListarTodos()
    {
        var pedidos = await _repository.Get();
        List<PedidoDTO> pedidosDTO = [];

        foreach (var pedido in pedidos)
        {
            pedidosDTO.Add(ConverterParaDTO(pedido));
        }

        return pedidosDTO;
    }

    public async Task<PedidoDTO> ObterPorId(int id)
    {
        var pedido = await _repository.GetById(id);

        if (pedido is null)
        {
            return null;
        }

        return ConverterParaDTO(pedido);
    }

    public async Task<PedidoDTO> GerarPedido(PedidoDTO pedidoDTO)
    {
        var pedido = ConverterParaModel(pedidoDTO);
        IFrete frete = CriarFretePorTipo(pedido.TipoFrete);

        pedido.EstadoAtual = EstadoPedido.AguardandoPagamento;
        pedido.ValorFrete = frete.CalcularFrete(pedido.Subtotal);

        await _repository.Add(pedido);
        return ConverterParaDTO(pedido);
    }

    public async Task<PedidoDTO> Atualizar(PedidoDTO pedidoDTO, int id)
    {
        var existingPedido = await _repository.GetById(id);

        if (existingPedido is null)
        {
            throw new KeyNotFoundException($"Pedido com id {id} não encontrado.");
        }

        if (existingPedido.EstadoAtual == EstadoPedido.AguardandoPagamento)
        {
            
            pedidoDTO.EstadoAtual = (int)existingPedido.EstadoAtual;

            
            IFrete frete = CriarFretePorTipo((TipoFrete)pedidoDTO.TipoFrete);
            pedidoDTO.ValorFrete = frete.CalcularFrete(pedidoDTO.Subtotal);
        } else
        {
            throw new Exception("Não é permitido atualizar o pedido, após sua confirmação/cancelamento.");
        }

        var pedido = ConverterParaModel(pedidoDTO);
        await _repository.Update(pedido);

        return pedidoDTO;
    }

    public async Task<PedidoDTO> SucessoAoPagar(PedidoDTO pedidoDTO)
    {
        
        IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);

        
        IPedidoState novoEstado = state.SucessoAoPagar();

        
        pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

        
        await _repository.Update(ConverterParaModel(pedidoDTO));

        return pedidoDTO;
    }

    public async Task<PedidoDTO> DespacharPedido(PedidoDTO pedidoDTO)
    {
        IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);
        IPedidoState novoEstado = state.DespacharPedido();
        pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

        await _repository.Update(ConverterParaModel(pedidoDTO));

        return pedidoDTO;
    }

    public async Task<PedidoDTO> CancelarPedido(PedidoDTO pedidoDTO)
    {
        IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);
        IPedidoState novoEstado = state.CancelarPedido();
        pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

        await _repository.Update(ConverterParaModel(pedidoDTO));

        return pedidoDTO;
    }

    #region Métodos de Conversão
    private IPedidoState ObterEstadoClasse(EstadoPedido estadoPedido)
    {
        return estadoPedido switch
        {
            EstadoPedido.AguardandoPagamento => new AguardandoPagamentoState(),
            EstadoPedido.Pago => new PagoState(),
            EstadoPedido.Enviado => new EnviadoState(),
            EstadoPedido.Cancelado => new CanceladoState(),
            _ => throw new ArgumentException("Estado inválido"),
        };
    }

    private EstadoPedido ObterEstadoEnum(IPedidoState state)
    {
        return state switch
        {
            AguardandoPagamentoState _ => EstadoPedido.AguardandoPagamento,
            PagoState _ => EstadoPedido.Pago,
            EnviadoState _ => EstadoPedido.Enviado,
            CanceladoState _ => EstadoPedido.Cancelado,
            _ => throw new ArgumentException("Estado inválido"),
        };
    }

    private IFrete CriarFretePorTipo(TipoFrete tipoFrete)
    {
        return tipoFrete switch
        {
            TipoFrete.Aereo => new FreteAereo(),
            TipoFrete.Terrestre => new FreteTerrestre(),
            _ => throw new ArgumentException("Tipo de frete inválido"),
        };
    }

    private PedidoDTO ConverterParaDTO(Pedido pedido)
    {
        PedidoDTO pedidoDTO = new()
        {
            Id = pedido.Id,
            Subtotal = pedido.Subtotal,
            ValorFrete = pedido.ValorFrete,
            EstadoAtual = (int)pedido.EstadoAtual,
            TipoFrete = (int)pedido.TipoFrete,
        };

        return pedidoDTO;
    }

    private Pedido ConverterParaModel(PedidoDTO pedidoDTO)
    {
        Pedido pedido = new()
        {
            Id = pedidoDTO.Id,
            Subtotal = pedidoDTO.Subtotal,
            ValorFrete = pedidoDTO.ValorFrete,
            EstadoAtual = (EstadoPedido)pedidoDTO.EstadoAtual,
            TipoFrete = (TipoFrete)pedidoDTO.TipoFrete,
        };

        return pedido;
    }
    #endregion
}