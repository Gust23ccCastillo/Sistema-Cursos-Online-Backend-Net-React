using Application.Automapper.Courses.Query;
using Application.Commands;
using Application.Commands.Course_Command;
using Application.Queries.Course_Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.BaseController;

namespace WebApi.Controllers.Courses
{

    [Route("api/[controller]")]
    [ApiController]


    public class CoursesController : Principal_Base_Controller
    {
        [HttpGet("GetAllCourseInSystem")]
        public async Task<ActionResult<List<CourseInformationDto>>> GetAllCourseInSystem(CancellationToken cancellationToken)
        {
            return await mediatorInject.Send(new GetAllListCourses.AllListCourseAsync(), cancellationToken);
        }

        [HttpGet("GetSpecificCourseInfo/{idParameter}")]
        public async Task<ActionResult<CourseInformationDto>> GetSpecificCourseInfo(Guid idParameter, CancellationToken cancellationToken)
        {
            return await mediatorInject.Send(new GetUniqueCourseByParameter.GetOneCourseByIdParameterAsync { IdParameter = idParameter }, cancellationToken);
        }

        [HttpPost("CreateCourseInformation")]
        public async Task<ActionResult<Unit>> CreateCourseInformation(CreateCourseInformation.NewInfoCourseAsync newInfoCourseParameter,
            CancellationToken cancellationToken)
        {
            return await mediatorInject.Send(newInfoCourseParameter, cancellationToken);
        }

        [HttpPut("UpdateCourseInformation/{idCourseParameter}")]
        public async Task<ActionResult<Unit>> UpdateCourseInformation(Guid idCourseParameter,
             UpdateCourseInformation.UpdateInformationForCourseAsync updateCourseParameter, CancellationToken cancellationToken)
        {
            updateCourseParameter.IdCourseParameter = idCourseParameter;
            return await mediatorInject.Send(updateCourseParameter, cancellationToken);
        }

        [HttpDelete("RemoveCourseInformation/{IdCourseParameter}")]
        public async Task<ActionResult<Unit>> RemoveCourseInformation(Guid IdCourseParameter, CancellationToken cancellationToken)
        {
            return await mediatorInject.Send(new DeleteCourseInformation.DeleteInfoCourseAsync { IdCourseParameter = IdCourseParameter }, cancellationToken);
        }
    }
}
