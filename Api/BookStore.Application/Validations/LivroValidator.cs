using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validations;

public class LivroValidator : AbstractValidator<LivroDTO>
{
    public LivroValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Título é obrigatório")
            .MaximumLength(40).WithMessage("Título deve ter no máximo 40 caracteres");

        RuleFor(x => x.Editora)
            .NotEmpty().WithMessage("Editora é obrigatória")
            .MaximumLength(40).WithMessage("Editora deve ter no máximo 40 caracteres");

        RuleFor(x => x.Edicao)
            .GreaterThan(0).WithMessage("Edição deve ser maior que zero");

        RuleFor(x => x.AnoPublicacao)
            .NotEmpty().WithMessage("Ano de publicação é obrigatório")
            .Length(4).WithMessage("Ano de publicação deve ter 4 caracteres");

        RuleFor(x => x.AutoresIds)
            .NotEmpty().WithMessage("Pelo menos um autor deve ser informado");

        RuleFor(x => x.AssuntosIds)
            .NotEmpty().WithMessage("Pelo menos um assunto deve ser informado");
    }
}