🛒 Wake Commerce API

API REST desenvolvida para o desafio técnico da Wake Commerce.
O projeto implementa um CRUD completo de produtos, utilizando .NET 8, Entity Framework Core e boas práticas de arquitetura.

🚀 Tecnologias Utilizadas

.NET 8 (Web API)

Entity Framework Core (Code-First + SQLite)

Swagger / OpenAPI para documentação

XUnit para testes unitários

FluentAssertions para validações

GitHub Actions para execução automática de testes (CI)

📦 Estrutura do Projeto
WakeCommerce.sln
│
├── WakeCommerce.Api          # Projeto principal da API
│   ├── Controllers           # Endpoints da aplicação
│   ├── Data                 # DbContext e seed de dados
│   ├── Models               # Entidades do domínio
│   └── Program.cs           # Configuração inicial
│
└── WakeCommerce.Tests        # Projeto de testes unitários e integração

🏗️ Funcionalidades

✅ Criar produto (validação para não permitir valor negativo)
✅ Atualizar produto
✅ Excluir produto
✅ Listar todos os produtos
✅ Visualizar produto por ID
✅ Ordenação dos produtos por diferentes campos (nome, valor, estoque)
✅ Busca de produto pelo nome
✅ Popular o banco de dados automaticamente com 5 produtos iniciais

🔧 Como Executar
1️⃣ Clonar o repositório
git clone https://github.com/seu-usuario/wake-commerce.git
cd wake-commerce

2️⃣ Restaurar dependências
dotnet restore

3️⃣ Aplicar as migrações e criar o banco de dados
cd WakeCommerce.Api
dotnet ef database update

4️⃣ Rodar a aplicação
dotnet run --project WakeCommerce.Api


A API estará disponível em:
📌 https://localhost:5001/swagger

🧪 Testes

Rodar os testes:

dotnet test


Inclui:

Testes unitários para a lógica de negócios

Testes de integração usando WebApplicationFactory

🌱 Seed de Dados

Ao iniciar a aplicação, o banco é populado automaticamente com 5 produtos de exemplo para facilitar o teste da API.