using FluentValidation;
using dotnet_webapi.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dotnet_webapi.Features.Personagens;

public static class PersonagemEndpoints
{
    private static readonly List<Personagem> Db = new();

    public static void MapPersonagensEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/personagens")
                       .WithTags("Personagens")
                       .WithOpenApi(); 

        
        
        group.MapGet("/", () => TypedResults.Ok(Db));

        
        group.MapGet("/{id}", Results<Ok<Personagem>, NotFound> (Guid id) =>
        {
            var personagem = Db.FirstOrDefault(p => p.Id == id);
            return personagem is not null 
                ? TypedResults.Ok(personagem) 
                : TypedResults.NotFound();
        });

        
        group.MapPost("/", Results<Created<Personagem>, ValidationProblem> (
            CriarPersonagemDto dto, 
            IValidator<CriarPersonagemDto> validator, 
            RequestContext context) =>
        {
            var resultado = validator.Validate(dto);

            if (!resultado.IsValid)
            {
                context.AddMetadata("FalhaValidacao", resultado.Errors.Select(e => e.ErrorMessage));
                return TypedResults.ValidationProblem(resultado.ToDictionary());
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
            context.AddMetadata("Banda", novoPersonagem.Propriedades.Banda);
            
            return TypedResults.Created($"/personagens/{novoPersonagem.Id}", novoPersonagem);
        });

        group.MapDelete("/{id}", Results<NoContent, NotFound> (Guid id) =>
        {
            var removido = Db.RemoveAll(p => p.Id == id);
            return removido > 0 ? TypedResults.NoContent() : TypedResults.NotFound();
        });
    }
}
