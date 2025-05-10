using Application.Commands;
using Application.Commands.Course_Command;
using Application.Queries.Course_Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    // http:localhost:5000/api/courses
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediatorInject;

        public CoursesController(IMediator mediatorInject)
        {
            _mediatorInject = mediatorInject;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAllCourseInSystem(CancellationToken cancellationToken)
        {
            return await _mediatorInject.Send(new GetAllListCourses.AllListCourseAsync(), cancellationToken);
        }

        [HttpGet("{idParameter}")]
        public async Task<ActionResult<Course>> GetSpecificCourseInfo(Guid idParameter, CancellationToken cancellationToken)
        {
            return await _mediatorInject.Send(new GetUniqueCourseByParameter
                .GetOneCourseByIdParameterAsync { IdParameter = idParameter}, cancellationToken);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateCourseInformation(CreateCourseInformation.NewInfoCourseAsync newInfoCourseParameter,
            CancellationToken cancellationToken)
        {
            return await _mediatorInject.Send(newInfoCourseParameter,cancellationToken);
        }

        [HttpPut("{idCourseParameter}")]
        public async Task<ActionResult<Course>> UpdateCourseInformation(Guid idCourseParameter, 
             UpdateCourseInformation.UpdateInformationForCourseAsync updateCourseParameter, CancellationToken cancellationToken)
        {
            updateCourseParameter.IdCourseParameter = idCourseParameter;
            return await _mediatorInject.Send(updateCourseParameter,cancellationToken);
        }

        [HttpDelete("{IdCourseParameter}")]
        public async Task<ActionResult<Unit>> RemoveCourseInformation(Guid IdCourseParameter, CancellationToken cancellationToken)
        {
            return await this._mediatorInject.Send(new DeleteCourseInformation.DeleteInfoCourseAsync
            { IdCourseParameter = IdCourseParameter }, cancellationToken);
        }
    }
}
