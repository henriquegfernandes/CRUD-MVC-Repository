# Refactor para Sistema de Gestão de Estoque

## O que foi realizado
- Migração dos projetos para `net10.0` (ASP.NET 10).
- Reestruturação para camadas com foco em Clean Architecture:
  - **Domain**: entidades, contratos e handlers de CQRS.
  - **DataAccess**: infraestrutura com Dapper e SQL Server.
  - **Web**: camada de apresentação como API REST enxuta.
- Implementação inicial de CQRS com comandos e queries separados:
  - `ListarProdutosEstoqueQuery`
  - `CriarProdutoCommand`
  - `AtualizarEstoqueCommand`
- Substituição da estratégia principal de acesso a dados para **Dapper**, reduzindo overhead de tracking do EF e diminuindo uso de memória/cpu.
- Program.cs simplificado com pipeline mínimo para API e OpenAPI em ambiente de desenvolvimento.

## Melhorias de performance aplicadas
- Acesso a dados por SQL explícito com Dapper (menor overhead que ORM completo).
- Paginação com `OFFSET/FETCH` para evitar consultas massivas.
- `CancellationToken` propagado em todos os fluxos de I/O.
- Limites de paginação para evitar abuso de recursos (`max 100` itens por página).
- Configuração de logs com nível mais conservador para reduzir ruído e custo de logging.

## Status atual
Este refactor entrega a **base arquitetural** para evolução do sistema de estoque. Ainda não é um ERP completo de loja de roupas.

## Próximos passos recomendados para ficar completo
1. **Módulo de catálogo**
   - Grade/tamanho/cor por produto (SKU variante).
   - Gestão de categorias, coleções e marca.
2. **Movimentações de estoque**
   - Entradas, saídas, ajustes, inventário cíclico.
   - Trilhas de auditoria e usuário responsável.
3. **Vendas e reservas**
   - Reserva temporária de estoque no carrinho.
   - Baixa definitiva após pagamento.
4. **Segurança e governança**
   - Autenticação JWT + refresh token.
   - RBAC por perfil (admin, operador, gerente).
5. **Qualidade e operação**
   - Testes unitários e integração com banco de testes.
   - Observabilidade (tracing + métricas + health checks).
   - CI/CD com validação automática de build/testes.
6. **Escala**
   - Cache de leitura para produtos populares.
   - Estratégia de concorrência otimista para evitar conflito de estoque.

