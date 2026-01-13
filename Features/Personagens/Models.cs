namespace dotnet_webapi.Features.Personagens;

public record Propriedades(string Instrumento, string Banda);

public record Personagem(Guid Id, string Nome, int Idade, string NomeArtistico, Propriedades Propriedades);

public record CriarPersonagemDto(string Nome, int Idade, string NomeArtistico, Propriedades Propriedades);
