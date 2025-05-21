 🛒 ECommerce Pedido Service - Projeto com Design Patterns

Este projeto simula o sistema de pedidos de um e-commerce, com foco no uso de padrões de projeto aplicados à lógica de negócio, incluindo controle de status e cálculo de frete.



 ⚙️ Funcionalidades Principais

  Controle de Status do Pedido:  
  Estados possíveis: `Aguardando Pagamento`, `Pago`, `Enviado`, `Cancelado`.

  Cálculo de Frete por Estratégia:  
    🚛 Terrestre: 5% do valor do pedido  
    ✈️ Aéreo: 10% do valor do pedido  

  Cancelamento Irreversível:  
  Após o cancelamento, o pedido não pode mais ser alterado.

  Arquitetura Flexível:  
  Sistema projetado para facilitar adição de novos tipos de frete e novos estados sem modificar o código já existente.

 🧠 Padrões de Projeto Utilizados

 🟨 State Pattern
    Gerencia os estados do pedido e controla quais ações são possíveis em cada estado.  
    Cada estado (`AguardandoPagamentoState`, `PagoState`, etc.) implementa a interface `IPedidoState`.

 🟦 Strategy Pattern
    Permite diferentes formas de cálculo de frete.  
    Cada implementação (`FreteAereo`, `FreteTerrestre`) aplica uma lógica distinta e intercambiável via `IFrete`.



 🧱 Arquitetura em Camadas

    O sistema foi implementado em ASP.NET Core com persistência em PostgreSQL, dividido em:



 1. Model

   Contém as entidades principais e enums utilizados no banco de dados.

📌 Diagrama de Classe - Model

![DiagramaModel](Diagramas/DiagramaModel.png)

> O modelo `Pedido` possui atributos como `id`, `valor`, `tipoFrete`, `status`, entre outros.  
> Os enums `EstadoPedido` e `TipoFrete` definem os valores válidos para status e tipo de entrega.



 2. Service

Contém as regras de negócio, aplicação dos padrões e orquestração de ações.

📌 Diagrama de Classe - Service

![DiagramaService](Diagramas/DiagramaService.png)

> O `PedidoService` é responsável por criar e gerenciar os pedidos, aplicar o frete adequado e controlar as transições de estado via State Pattern.




