using FluentValidation.TestHelper;
using dotnet_webapi.Features.Personagens; 

namespace dotnet_webapi.Tests;

public class CriarPersonagemValidatorTest
{
    private readonly CriarPersonagemValidator _validator;

    public CriarPersonagemValidatorTest()
    {
        _validator = new CriarPersonagemValidator();
    }

    [Fact]
    public void Deve_Ter_Erro_Quando_Nome_For_Vazio()
    {
        var model = new CriarPersonagemDto("", 20, "Slash", new Propriedades("Guitarra", "Guns"));
        
        var result = _validator.TestValidate(model);
        
        result.ShouldHaveValidationErrorFor(p => p.Nome);
    }

    [Fact]
    public void Deve_Ter_Erro_Quando_Idade_For_Negativa()
    {
        var model = new CriarPersonagemDto("Axl", -1, "Axl Rose", new Propriedades("Voz", "Guns"));
        
        var result = _validator.TestValidate(model);
        
        result.ShouldHaveValidationErrorFor(p => p.Idade)
              .WithErrorMessage("A idade deve ser maior que zero.");
    }

    [Fact]
    public void Deve_Exigir_NomeArtistico_Se_Maior_De_Idade()
    {
        var model = new CriarPersonagemDto("Saul", 50, "", new Propriedades("Guitarra", "Guns"));

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(p => p.NomeArtistico)
              .WithErrorMessage("Maiores de idade precisam de um nome artístico.");
    }

    [Fact]
    public void Nao_Deve_Exigir_NomeArtistico_Se_Menor_De_Idade()
    {
        var model = new CriarPersonagemDto("Menino", 10, "", new Propriedades("Flauta", "Escola"));

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(p => p.NomeArtistico);
    }
}
