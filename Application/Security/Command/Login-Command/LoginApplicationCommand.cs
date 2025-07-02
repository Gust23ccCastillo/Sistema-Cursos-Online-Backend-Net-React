using System.Net;
using Application.Automapper.Security.Command;
using Application.Contracts;
using Application.ModelCaptureException;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Login_Command
{
    public class LoginApplicationCommand
    {
        public class LoginAccountAsync : IRequest<UserInformationDto>
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class CommandHandler : IRequestHandler<LoginAccountAsync, UserInformationDto>
        {
            private readonly IJwtGenerateContract _jwtGenerateContractInject;
            private readonly UserManager<UserApplication> _userManagerInject;
            private readonly SignInManager<UserApplication> _signInManagerInject;

            public CommandHandler(IJwtGenerateContract JwtGenerateContractInject,
                UserManager<UserApplication> UserManagerInject, 
                SignInManager<UserApplication> SignInManagerInject)
            {
                this._jwtGenerateContractInject = JwtGenerateContractInject;
                this._userManagerInject = UserManagerInject;
                this._signInManagerInject = SignInManagerInject;
            }

            public async Task<UserInformationDto> Handle(LoginAccountAsync request, CancellationToken cancellationToken)
            {
                var searh_UserAccount_Email = await this._userManagerInject.FindByEmailAsync(request.Email);
                if (searh_UserAccount_Email == null) {
                    throw new CaptureExceptions(HttpStatusCode.Unauthorized,
                        "Email / Contrasena Incorrecta!!, Porfavor intentalo nuevamente.");
                }

                var check_Password_UserAccount = await this._signInManagerInject.CheckPasswordSignInAsync(searh_UserAccount_Email, request.Password,false);
                if (!check_Password_UserAccount.Succeeded)
                {
                    throw new CaptureExceptions(HttpStatusCode.Unauthorized,
                        "Email / Contrasena Incorrecta!!, Porfavor intentalo nuevamente.");
                }

                return new UserInformationDto
                {
                    FullName = searh_UserAccount_Email.FullName,
                    TokenAccess = _jwtGenerateContractInject.CreateTokenSecurityApplication(searh_UserAccount_Email),
                    UserName = searh_UserAccount_Email.UserName,
                    Email = searh_UserAccount_Email.Email,
                    UrlImage = null
                };

            }
        }


    }
}
