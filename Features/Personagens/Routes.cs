using FluentValidation;
using dotnet_webapi.Shared;

namespace dotnet_webapi.Features.Personagens;


public static class PersonagemEndpoints
{
    // Simulação de banco de dados isolada nesta feature
    private static readonly List<Personagem> Db = new();

    public static void MapPersonagensEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/personagens")
                       .WithTags("Personagens");

        // GET: Listar
        group.MapGet("/", () => Results.Ok(Db));

        // GET: Buscar por ID
        group.MapGet("/{id}", (Guid id) =>
        {
            var personagem = Db.FirstOrDefault(p => p.Id == id);
            return personagem is not null ? Results.Ok(personagem) : Results.NotFound();
        });

        // POST: Criar
        group.MapPost("/", (CriarPersonagemDto dto, IValidator<CriarPersonagemDto> validator, RequestContext context) =>
        {
            var resultado = validator.Validate(dto);

            if (!resultado.IsValid)
            {
                context.AddMetadata("FalhaValidacao", resultado.Errors.Select(e => e.ErrorMessage));
                return Results.ValidationProblem(resultado.ToDictionary());
            }

            var novoPersonagem = new Personagem(
                Guid.NewGuid(),
                dto.Nome,
                dto.Idade,
                dto.NomeArtistico,
                dto.Propriedades
            );

            Db.Add(novoPersonagem);

            context.AddMetadata("PersonagemId", novoPersonagem.Id);
            context.AddMetadata("BandaEscolhida", novoPersonagem.Propriedades.Banda);
            context.AddMetadata("LogicaDeNegocio", "Criação bem sucedida via Minimal API");
            return Results.Created($"/personagens/{novoPersonagem.Id}", novoPersonagem);
        });

        // PATCH: Atualizar
        group.MapPatch("/{id}", (Guid id, CriarPersonagemDto dto) =>
        {
            var index = Db.FindIndex(p => p.Id == id);
            if (index == -1) return Results.NotFound();

            var personagemAtualizado = new Personagem(
                id,
                dto.Nome,
                dto.Idade,
                dto.NomeArtistico,
                dto.Propriedades
            );

            Db[index] = personagemAtualizado;
            return Results.Ok(personagemAtualizado);
        });

        // DELETE: Remover
        group.MapDelete("/{id}", (Guid id) =>
        {
            var removido = Db.RemoveAll(p => p.Id == id);
            return removido > 0 ? Results.NoContent() : Results.NotFound();
        });
    }
}
