using FluentValidation;

namespace dotnet_webapi.Features.Personagens;

// 1. Validador para o DTO Principal
public class CriarPersonagemValidator : AbstractValidator<CriarPersonagemDto>
{
    public CriarPersonagemValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 letras.");

        RuleFor(x => x.Idade)
            .GreaterThan(0).WithMessage("A idade deve ser maior que zero.")
            .LessThan(150).WithMessage("Calma lá, ninguém vive tanto assim.");

        RuleFor(x => x.NomeArtistico)
            .NotEmpty().When(x => x.Idade > 18)
            .WithMessage("Maiores de idade precisam de um nome artístico.");

        // Validação do Objeto Aninhado (Propriedades)
        RuleFor(x => x.Propriedades)
            .NotNull().WithMessage("As propriedades do personagem são obrigatórias.")
            .SetValidator(new PropriedadesValidator());
    }
}

// 2. Validador para o Objeto Aninhado
public class PropriedadesValidator : AbstractValidator<Propriedades>
{
    public PropriedadesValidator()
    {
        RuleFor(x => x.Instrumento)
            .NotEmpty().WithMessage("Informe o instrumento.");

        RuleFor(x => x.Banda)
            .NotEmpty().WithMessage("A banda não pode ser vazia.");
    }
}
