using System.Net;
using Application.ModelCaptureException;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContextApplication;

namespace Application.Security.Login_Command
{
    public class LoginApplicationCommand
    {
        public class LoginAccount : IRequest<UserApplication>
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class CommandHandler : IRequestHandler<LoginAccount, UserApplication>
        {
            private readonly CourseOnlineDbContext courseOnlineDbContextInject;
            private readonly UserManager<UserApplication> userManagerInject;
            private readonly SignInManager<UserApplication> signInManagerInject;

            public CommandHandler(CourseOnlineDbContext courseOnlineDbContextInject,
                UserManager<UserApplication> userManagerInject, 
                SignInManager<UserApplication> signInManagerInject)
            {
                this.courseOnlineDbContextInject = courseOnlineDbContextInject;
                this.userManagerInject = userManagerInject;
                this.signInManagerInject = signInManagerInject;
            }

            public async Task<UserApplication> Handle(LoginAccount request, CancellationToken cancellationToken)
            {
                var searh_UserAccount_Email = await this.userManagerInject.FindByEmailAsync(request.Email);
                if (searh_UserAccount_Email == null) {
                    throw new CaptureExceptions(HttpStatusCode.Unauthorized,
                        "Email / Contrasena Incorrecta!!, Porfavor intentalo nuevamente.");
                }

                var check_Password_UserAccount = await this.signInManagerInject.CheckPasswordSignInAsync(searh_UserAccount_Email, request.Password,false);
                if (!check_Password_UserAccount.Succeeded)
                {
                    throw new CaptureExceptions(HttpStatusCode.Unauthorized,
                        "Email / Contrasena Incorrecta!!, Porfavor intentalo nuevamente.");
                }

                return searh_UserAccount_Email;

            }
        }


    }
}
