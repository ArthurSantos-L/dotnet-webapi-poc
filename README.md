# DotNet WebAPI POC - Vertical Slice Architecture

Esta é uma Prova de Conceito (POC) de uma API RESTful robusta desenvolvida com **.NET 8**, utilizando **Minimal APIs** e organizada sob o padrão de arquitetura **Vertical Slice** (Fatias Verticais).

O projeto demonstra práticas modernas de desenvolvimento, incluindo validação de dados fortemente tipada, testes unitários e um sistema de logs estruturados customizado.

## 🚀 Tecnologias e Bibliotecas

* 
**[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**: Framework base do projeto.


* 
**Minimal APIs**: Para definição de endpoints leves e performáticos sem o uso de Controllers tradicionais.


* 
**[FluentValidation](https://www.google.com/search?q=https://docs.fluentvalidation.net/)**: Para validação de regras de negócios e DTOs.


* 
**xUnit & FluentAssertions**: Para testes unitários assertivos e legíveis.


* 
**Swagger/OpenAPI**: Para documentação e testes interativos da API.



## 📂 Arquitetura (Vertical Slices)

Diferente da arquitetura em camadas tradicional (Controller, Service, Repository), este projeto agrupa o código por **Funcionalidade (Feature)**. Tudo o que é necessário para uma feature funcionar está contido em sua respectiva pasta:

```text
dotnet-webapi/
├── Features/
│   └── Personagens/           # Feature: Gerenciamento de Personagens
│       ├── Models.cs          # Records/DTOs (Entrada e Saída)
│       ├── Routes.cs          # Definição das Rotas (Endpoints)
│       └── Validator.cs       # Regras de Validação (FluentValidation)
├── Shared/                    # Componentes transversais (Logs, Contexto)
└── dotnet-webapi.Tests/       # Projeto de Testes Unitários

```

## ✨ Funcionalidades Principais

### 1. Log Estruturado com Contexto

A aplicação possui um Middleware customizado (`LogMiddleware`) que intercepta todas as requisições para medir tempo de execução e capturar status HTTP.

Foi implementado um **`RequestContext`** (Scoped) que viaja com a requisição, permitindo que qualquer parte do código injete metadados no log final.

Exemplo de uso nas rotas:

```csharp
// Injetando dados de negócio no log durante a execução da rota
context.AddMetadata("PersonagemId", novoPersonagem.Id);
context.AddMetadata("Banda", novoPersonagem.Propriedades.Banda);

```

### 2. Validação Robusta

Os dados de entrada (`CriarPersonagemDto`) são validados automaticamente antes do processamento. Regras implementadas incluem:

* Nome obrigatório com tamanho mínimo.


* Idade deve ser positiva e menor que 150.


* 
**Validação Condicional**: "Nome Artístico" é obrigatório apenas se a idade for maior que 18.


* Validação de objetos aninhados (`Propriedades` da banda).



## ⚙️ Como Executar

### Pré-requisitos

* .NET 8 SDK instalado.

### Rodando a API

1. Navegue até a pasta raiz da solução.
2. Execute o projeto da API:
```bash
dotnet run --project dotnet-webapi/dotnet-webapi.csproj

```


Ou para desenvolvimento com *hot-reload*:
```bash
dotnet watch run --project dotnet-webapi/dotnet-webapi.csproj

```


3. Acesse o Swagger no navegador:
* URL: `http://localhost:5070/swagger`.





### Rodando os Testes

O projeto contém testes unitários que garantem a integridade das regras de validação.

1. Execute o comando de teste na raiz:
```bash
dotnet test

```



## 📝 Endpoints Disponíveis

A feature **Personagens** expõe os seguintes endpoints:

* `GET /personagens`: Retorna a lista de personagens.
* `GET /personagens/{id}`: Busca um personagem por ID.
* `POST /personagens`: Cria um novo personagem (Requer JSON válido).
* `DELETE /personagens/{id}`: Remove um personagem.

---

*Projeto desenvolvido como Prova de Conceito (POC).*
