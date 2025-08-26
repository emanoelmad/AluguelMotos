# AluguelMotos API

## Descrição
API para gestão de aluguel de motos, entregadores, planos de locação e integrações com RabbitMQ e banco de dados.

## Como rodar

1. **Banco de Dados (PostgreSQL):**
   - Suba o banco com Docker:
     ```powershell
     docker run --name postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres
     ```
   - Configure a string de conexão em `appsettings.json`.
   - Execute as migrations:
     ```powershell
     dotnet ef database update --project AluguelMotos.Infrastructure --startup-project AluguelMotos.Api
     ```

2. **RabbitMQ:**
   - Suba o RabbitMQ com Docker:
     ```powershell
     docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
     ```
   - Painel: [http://localhost:15672](http://localhost:15672) (guest/guest)

3. **Rodar a API:**
   ```powershell
   dotnet run --project AluguelMotos.Api
   ```
   - Acesse o Swagger em `/swagger` para testar os endpoints.

## Testes
- Teste unitário de cadastro de moto disponível em `AluguelMotos.Api.Tests/MotosControllerTests.cs`.
- Para rodar os testes:
  ```powershell
  dotnet test AluguelMotos.Api.Tests
  ```

## Arquitetura
- **Domain:** Entidades e interfaces
- **Infrastructure:** Persistência e repositórios
- **Api:** Controllers, Models, Middlewares
- **Design Pattern:** Repository

## Diferenciais implementados
- EntityFramework
- Docker e Docker Compose
- Design Patterns
- Middleware de tratamento de erros
- Logs básicos
- Teste unitário
- Código limpo e organizado
- Convenções da comunidade

## Endpoints principais
- `/motos` — CRUD de motos
- `/entregadores` — CRUD de entregadores
- `/entregadores/{id}/cnh` — Upload CNH
- `/locacao` — CRUD de locações
- `/api/planos-locacao` — CRUD de planos de locação

---
Para dúvidas ou melhorias, abra uma issue ou envie sugestões!
