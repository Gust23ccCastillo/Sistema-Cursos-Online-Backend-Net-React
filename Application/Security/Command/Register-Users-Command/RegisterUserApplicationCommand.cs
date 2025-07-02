using System.Net;
using Application.Automapper.Security.Command;
using Application.Contracts;
using Application.ModelCaptureException;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Security.Register_Users_Command
{
    public class RegisterUserApplicationCommand
    {
        public class RegisterAccountAsync : IRequest<UserInformationDto>
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class CommandHandler : IRequestHandler<RegisterAccountAsync, UserInformationDto>
        {
            private readonly IJwtGenerateContract _jwtGenerateContract;
            private readonly UserManager<UserApplication> _userManager;
            private readonly CourseOnlineDbContext _courseOnlineDbContext;

            public CommandHandler(IJwtGenerateContract jwtGenerateContract, 
                UserManager<UserApplication> userManager,
                CourseOnlineDbContext courseOnlineDbContext)
            {
                _jwtGenerateContract = jwtGenerateContract;
                _userManager = userManager;
                _courseOnlineDbContext = courseOnlineDbContext;
            }

            public async Task<UserInformationDto> Handle(RegisterAccountAsync request, CancellationToken cancellationToken)
            {
                var validateExistingEmail = await _courseOnlineDbContext.tb_UserApplication
                     .Where(search => search.Email == request.Email)
                     .AnyAsync();
                if (validateExistingEmail == true)
                {
                    throw new CaptureExceptions(HttpStatusCode.BadRequest,
                        new { messageInformation = $"El email: {request.Email}, Ya se encuentra registrado, Porfavor ingresar otro Emial." });
                }

                var validateExistingFullName = await _courseOnlineDbContext.tb_UserApplication
                    .Where(search => search.FullName == request.FirstName + " " + request.LastName)
                    .AnyAsync();
                if (validateExistingFullName == true)
                {
                    throw new CaptureExceptions(HttpStatusCode.BadRequest,
                       new { messageInformation = $"El Nombre de Usuario: {request.FirstName + " " + request.LastName},Ya cuenta con una cuenta registrada!!." });
                }

                var newUser = new UserApplication
                {
                    FullName = request.FirstName + " " + request.LastName,
                    Email = request.Email,
                    UserName = request.FirstName
                };

                var ResultToOperation = await _userManager.CreateAsync(newUser, request.Password);
                if (ResultToOperation.Succeeded)
                {
                    return new UserInformationDto
                    {
                        FullName = newUser.FullName,
                        UserName = newUser.UserName,
                        Email = newUser.Email,  
                        TokenAccess = _jwtGenerateContract.CreateTokenSecurityApplication(newUser),
                    };
                }

                throw new CaptureExceptions(HttpStatusCode.InternalServerError,
                      new { messageInformation = "No se pudo crear el usuario, Problemas con el servidor!!."});
            }
        }
    }
}
