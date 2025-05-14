using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Principal_Base_Controller : ControllerBase
    {
        private IMediator _mediatorInject;
        protected IMediator mediatorInject => _mediatorInject ?? (_mediatorInject = HttpContext.RequestServices.GetService<IMediator>()); 
    }
}
