using FluentValidation;
using Manager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                  .NotEmpty()
                  .WithMessage("A entidade não pode ser  vazia")
                  .NotNull()
                  .WithMessage("Menssagem não pode ser nula");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O Nome não pode ser vazio")
                .NotNull()
                .WithMessage("Nome não pode ser nula")
                .MinimumLength(3)
                .WithMessage("O nome deve ter no minimo 3 caracteres")
                .MaximumLength(80)
                .WithMessage("O nome deve ter no maximo 80 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O Email não pode ser vazio")
                .NotNull()
                .WithMessage("Email não pode ser nula")
                .MinimumLength(10)
                .WithMessage("O Email deve ter no minimo 10 caracteres")
                .MaximumLength(180)
                .WithMessage("O nome deve ter no maximo 180 caracteres")
                .EmailAddress()
                .WithMessage("Digite um Email Valido")
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O email informado não é válido2.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ser vazio")
                .NotNull()
                .WithMessage("A senha não pode ser nula")
                .MinimumLength(6)
                .WithMessage("O nome deve ter no minimo 6 caracteres")
                .MaximumLength(30)
                .WithMessage("O nome deve ter no maximo 30 caracteres");

        }

    }
}
