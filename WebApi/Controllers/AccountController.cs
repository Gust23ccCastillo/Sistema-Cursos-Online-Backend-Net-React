using Application.Security.Login_Command;
using Application.Security.Login_Command.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : Principal_Base_Controller
    {
        [HttpPost("login-Account")]
        public async Task<ActionResult<UserInformationDto>> Login_Account(LoginApplicationCommand.LoginAccount loginParameters)
        {
            return await mediatorInject.Send(loginParameters);
        }
        
    }
}
