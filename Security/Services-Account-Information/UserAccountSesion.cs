using System.Security.Claims;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Security
{
    public class UserAccountSesion : IUserAccountSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessorInject;

        public UserAccountSesion(IHttpContextAccessor httpContextAccessorInject)
        {
            _httpContextAccessorInject = httpContextAccessorInject;
        }

        //Buscar en el claim el username
        public string GetUserAccountSesion()
        {
            var userName = _httpContextAccessorInject.HttpContext.User?
                 .Claims?.FirstOrDefault(search => search.Type == ClaimTypes.NameIdentifier)?.Value;

            return userName;
        }
    }
}
