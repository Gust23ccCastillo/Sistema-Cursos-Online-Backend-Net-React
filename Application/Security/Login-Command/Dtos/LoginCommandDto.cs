using FluentValidation;
using static Application.Security.Login_Command.LoginApplicationCommand;

namespace Application.Security.Login_Command.Dtos
{
    public class LoginCommandDto:AbstractValidator<LoginAccount>
    {
        public LoginCommandDto() {
            RuleFor(parameter => parameter.Email)
                  .NotEmpty().WithMessage("Debe ingresar una email!!")
                  .EmailAddress().WithMessage("Debe ingresaer un email valido!!");

            RuleFor(parameter => parameter.Password)
                .NotEmpty().WithMessage("Debe ingresar una password!!");
          
        }

    }
}
