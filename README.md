WakeCommerce API

API REST para gerenciamento de produtos, seguindo a arquitetura Controller → Service → Repository, com .NET 8 e EF Core.

1️⃣ Estrutura do Projeto
src/
├─ WakeCommerce.Api/                  # API principal
│  ├─ Controllers/
│  │  └─ ProdutoController.cs
│  ├─ Services/
│  │  ├─ Interfaces/
│  │  │  └─ IProdutoService.cs
│  │  └─ ProdutoService.cs
│  └─ WakeCommerce.Api.csproj
├─ WakeCommerce.Domain/               # Entidades do domínio
│  ├─ Models/
│  │  └─ Produto.cs
│  └─ WakeCommerce.Domain.csproj
├─ WakeCommerce.Infrastructure/       # Repositórios e acesso a dados
│  ├─ Interface/
│  │  └─ IProdutoRepository.cs
│  └─ WakeCommerce.Infrastructure.csproj
├─ WakeCommerce.Service/              # Serviços que encapsulam regras de negócio
│  ├─ Interfaces/
│  │  └─ IProdutoService.cs
│  └─ ProdutoService.cs
│  └─ WakeCommerce.Service.csproj

2️⃣ Tecnologias

.NET 8

C# 12

Entity Framework Core (InMemory / Sqlite / SQL Server)

Swashbuckle (Swagger)

Arquitetura em Camadas (API, Service, Domain, Infrastructure)

4️⃣ Endpoints principais
Produtos
Método	Endpoint	Descrição
GET	/api/produto	Retorna todos os produtos
GET	/api/produto/{id}	Retorna produto pelo ID
POST	/api/produto	Cria um novo produto
PUT	/api/produto/{id}	Atualiza um produto existente
DELETE	/api/produto/{id}	Deleta um produto pelo ID

Todos os endpoints retornam códigos HTTP apropriados: 200, 204, 400, 404.

5️⃣ Swagger / Documentação

A API possui Swagger integrado.
Para acessar:

http://localhost:5042/swagger

Os summaries nos controllers são exibidos

Parâmetros e tipos de retorno estão documentados

Permite testar os endpoints diretamente pela interface web