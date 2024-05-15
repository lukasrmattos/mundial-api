using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UsuarioDTOValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("É obrigatório o fornecimento do Nome do Usuário.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("É obrigatório o fornecimento do E-mail do Usuário.")
                .EmailAddress().WithMessage("O E-mail fornecido não é válido.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("É obrigatório o fornecimento da Senha do Usuário.")
                .MinimumLength(6).WithMessage("A Senha deve conter no mínimo 6 caracteres.")
                .MaximumLength(20).WithMessage("A Senha deve conter no máximo 20 caracteres.");

            RuleFor(x => x.Perfil).NotNull().When(x => x.Perfil != null);
        }
    }
}
