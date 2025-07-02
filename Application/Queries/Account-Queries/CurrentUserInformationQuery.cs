using Application.Automapper.Security.Command;
using Application.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries.Account_Queries
{
    public class CurrentUserInformationQuery
    {
        public class CurrentUserAsync : IRequest<UserInformationDto> { }

        public class QueryHandler : IRequestHandler<CurrentUserAsync, UserInformationDto>
        {
            private readonly UserManager<UserApplication> _userManagerInject;
            private readonly IJwtGenerateContract _jwtGenerateContractInject;
            private readonly IUserAccountSesion _userAccountSesionInject;

            public QueryHandler(UserManager<UserApplication> userManagerInject, 
                IJwtGenerateContract jwtGenerateContractInject, 
                IUserAccountSesion userAccountSesionInject)
            {
                _userManagerInject = userManagerInject;
                _jwtGenerateContractInject = jwtGenerateContractInject;
                _userAccountSesionInject = userAccountSesionInject;
            }

            public async Task<UserInformationDto> Handle(CurrentUserAsync request, CancellationToken cancellationToken)
            {
                var CurrentUser = await this._userManagerInject.FindByNameAsync(_userAccountSesionInject.GetUserAccountSesion());

                return new UserInformationDto
                {
                    FullName = CurrentUser.FullName,
                    UserName = CurrentUser.UserName,
                    TokenAccess = this._jwtGenerateContractInject.CreateTokenSecurityApplication(CurrentUser),
                    Email = CurrentUser.Email,
                    UrlImage = null
                };
            }
        }
    }
}
