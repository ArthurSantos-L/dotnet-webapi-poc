namespace dotnet_webapi.Features.Personagens;

/// <summary>
/// Detalhes musicais do personagem.
/// </summary>
public record Propriedades(string Instrumento, string Banda);

/// <summary>
/// Representa um personagem persistido no banco.
/// </summary>
public record Personagem(Guid Id, string Nome, int Idade, string NomeArtistico, Propriedades Propriedades);

/// <summary>
/// DTO para criação de novos personagens.
/// </summary>
public record CriarPersonagemDto(string Nome, int Idade, string NomeArtistico, Propriedades Propriedades);
