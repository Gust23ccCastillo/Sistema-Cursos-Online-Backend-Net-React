using Application.Security.Login_Command;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : Principal_Base_Controller
    {
        [HttpPost("login-Account")]
        public async Task<ActionResult<UserApplication>> Login_Account(LoginApplicationCommand.LoginAccount loginParameters)
        {
            return await mediatorInject.Send(loginParameters);
        }
        
    }
}
