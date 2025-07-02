using Application.Automapper.Security.Command;
using Application.Queries.Account_Queries;
using Application.Security.Login_Command;
using Application.Security.Register_Users_Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.BaseController;

namespace WebApi.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : Principal_Base_Controller
    {
        [HttpPost("login-Account")]
        [AllowAnonymous]
        public async Task<ActionResult<UserInformationDto>> Login_Account(LoginApplicationCommand.LoginAccountAsync loginParameters)
        {
            return await mediatorInject.Send(loginParameters);
        }

        [HttpPost("Register-Account")]
        [AllowAnonymous]
        public async Task<ActionResult<UserInformationDto>> Register_Account(RegisterUserApplicationCommand.RegisterAccountAsync registerAccountParamters)
        {
            return await mediatorInject.Send(registerAccountParamters);
        }

        [HttpGet("GetUserAccount")]
        public async Task<ActionResult<UserInformationDto>> GetUserAccountInformation()
        {
            return await mediatorInject.Send(new CurrentUserInformationQuery.CurrentUserAsync());
        }

    }
}
