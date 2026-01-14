# DotNet WebAPI POC - Vertical Slice Architecture

Esta é uma Prova de Conceito (POC) de uma API RESTful utilizando .NET 8, Minimal APIs e arquitetura baseada em Features.

## 🚀 Tecnologias

- **.NET 8** (Minimal APIs)
- **FluentValidation**: Para validação de DTOs fortemente tipados.
- **Log Estruturado**: Middleware customizado com Contexto de Requisição.
- **Swagger/OpenAPI**: Documentação automática.

## 📂 Estrutura do Projeto

O projeto segue a **Vertical Slice Architecture**, onde tudo relacionado a uma funcionalidade fica na mesma pasta:

```text
dotnet-webapi/
├── Features/
│   └── Personagens/       # Feature Completa
│       ├── Models.cs      # Records/DTOs [cite: 6]
│       ├── Routes.cs      # Endpoints (Controller-less) [cite: 8]
│       └── Validator.cs   # Regras de Negócio 
├── Shared/
│   ├── LogMiddleware.cs   # Pipeline de Log Centralizado [cite: 27]
│   └── RequestContext.cs  # Estado Global da Requisição 
└── Program.cs             # Composição da Aplicação
