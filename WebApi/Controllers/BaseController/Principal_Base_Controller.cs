using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.BaseController
{
    [Authorize(Policy = "Bearer")]
    public class Principal_Base_Controller : ControllerBase
    {
        private IMediator _mediatorInject;
        protected IMediator mediatorInject => _mediatorInject ?? (_mediatorInject = HttpContext.RequestServices.GetService<IMediator>());
    }
}
