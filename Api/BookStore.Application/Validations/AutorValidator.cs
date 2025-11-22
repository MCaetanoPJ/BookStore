using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validations;

public class AutorValidator : AbstractValidator<AutorDTO>
{
    public AutorValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(40).WithMessage("Nome deve ter no máximo 40 caracteres");
    }
}