using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validations;

public class AssuntoValidator : AbstractValidator<AssuntoDTO>
{
    public AssuntoValidator()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .MaximumLength(20).WithMessage("Descrição deve ter no máximo 20 caracteres");
    }
}