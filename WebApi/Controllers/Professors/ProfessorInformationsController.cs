using Application.Commands.Professor_Info_Command;
using Application.Queries.Professor_Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence.Dapper_Configuration.Dtos;
using WebApi.Controllers.BaseController;

namespace WebApi.Controllers.Professors
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorInformationsController : Principal_Base_Controller
    {
        [HttpGet("List-Professor-Info")]
        public async Task<ActionResult<List<ProfessorDto>>> ObtainListProfessor()
        {
            return await mediatorInject.Send(new GetListProfessorInSystem.GetListAsync());
        }

        [HttpGet("Get-Professor-By-Parameter/{IdParameter}")]
        public async Task<ActionResult<ProfessorDto>> ObtainByIdInfoProfessor(Guid IdParameter)
        {
            return await this.mediatorInject.Send(new GetProfessorByParameter.GetInformationById { IdParameter = IdParameter });
        }

        [HttpPost("Create-Professor-Info")]
        public async Task<ActionResult<Unit>> CreateInformationProfessor(CreateProfessorInformation.NewProfessorInfoAsync newProfessorInfo)
        {
            return await mediatorInject.Send(newProfessorInfo);
        }

        [HttpPut("Update-Professor-Info/{IdParameter}")]
        public async Task<ActionResult<Unit>> UpdateInforamtionProfessor(Guid IdParameter, 
            EditProfessorInformation.UpdateInformationAsync updateInformation)
        {
            updateInformation.IdProfessor = IdParameter;
            return await mediatorInject.Send(updateInformation);
        }

        [HttpDelete("Delete-Professor-Info/{IdParameter}")]
        public async Task<ActionResult<Unit>> RemoveInformationProfessor(Guid IdParameter)
        {
            return await mediatorInject.Send(new RemoveProfessorInformation.DeleteInformationAsync { IdProfessor = IdParameter });
        }
    }
}
