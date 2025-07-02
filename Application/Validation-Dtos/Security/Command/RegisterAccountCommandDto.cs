using FluentValidation;
using static Application.Security.Register_Users_Command.RegisterUserApplicationCommand;

namespace Application.Automapper.Security.Command
{
    public class RegisterAccountCommandDto : AbstractValidator<RegisterAccountAsync>
    {
        public RegisterAccountCommandDto()
        {
            RuleFor(parameter => parameter.FirstName)
                .NotEmpty().WithMessage("Debe ingresar un nombre!!.")
                .MinimumLength(5).WithMessage("Minimo de caracteres para el nombre es de: '4'.");

            RuleFor(parameter => parameter.LastName)
                .NotEmpty().WithMessage("Debe ingresar un apellido!!.")
                .MinimumLength(5).WithMessage("Minimo de caracteres para el apellido es de: '4'.");

            RuleFor(parameter => parameter.Email)
                 .NotEmpty().WithMessage("Debe ingresar una email!!.")
                 .EmailAddress().WithMessage("Debe ingresaer un email valido!!.");

            RuleFor(parameter => parameter.Password)
                .NotEmpty().WithMessage("Debe ingresar una password!!.")
                .MinimumLength(7).WithMessage("La password debe ser como minimo de '7' caracteres!!.");

        }
    }
}
