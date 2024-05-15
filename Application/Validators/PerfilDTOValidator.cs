using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class PerfilDTOValidator : AbstractValidator<PerfilDTO>
    {
        public PerfilDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("É obrigatório fornecer o nome de um perfil.")
                .MinimumLength(1).WithMessage("O nome do perfil deve ter pelo menos 1 caractere.");

        }
    }
}
