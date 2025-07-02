using Application.ModelCaptureException;
using AutoMapper;
using MediatR;
using Persistence.Dapper_Configuration.Dtos;
using Persistence.Dapper_Configuration.Interfaces;

namespace Application.Commands.Professor_Info_Command
{
    public class EditProfessorInformation
    {
        public class UpdateInformationAsync : IRequest<Unit>
        {
            public Guid IdProfessor {  get; set; }
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Grade { get; set; }
        }

        public class CommandHandler : IRequestHandler<UpdateInformationAsync, Unit>
        {
            private readonly IProfessorServices _professorServicesInject;
            private readonly IMapper _autoMapperInject;

            public CommandHandler(IProfessorServices professorServicesInject, 
                IMapper autoMapperInject)
            {
                _professorServicesInject = professorServicesInject;
                _autoMapperInject = autoMapperInject;
            }

            public async Task<Unit> Handle(UpdateInformationAsync request, CancellationToken cancellationToken)
            {
                var updateInformationModel = this._autoMapperInject.Map<ProfessorDto>(request);
                updateInformationModel.ProfessorId = request.IdProfessor;
                var Result_To_Operation = await _professorServicesInject.UpdateProfessorInSystem(updateInformationModel);
                if(Result_To_Operation > 0)
                {
                    return Unit.Value;
                }

                throw new CaptureExceptions(System.Net.HttpStatusCode.InternalServerError,
                    new { messageInformation = "Ocurrio un Error!, No se pudo actualizar la informacion de los 'INSTRUCTORES..'" });
            }
        }
    }
}
