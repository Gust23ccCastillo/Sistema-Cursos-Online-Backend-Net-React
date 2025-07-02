using FluentValidation;
using static Application.Security.Login_Command.LoginApplicationCommand;

namespace Application.Automapper.Security.Command
{
    public class LoginCommandDto : AbstractValidator<LoginAccountAsync>
    {
        public LoginCommandDto()
        {
            RuleFor(parameter => parameter.Email)
                  .NotEmpty().WithMessage("Debe ingresar una email!!")
                  .EmailAddress().WithMessage("Debe ingresaer un email valido!!");

            RuleFor(parameter => parameter.Password)
                .NotEmpty().WithMessage("Debe ingresar una password!!");

        }

    }
}
