using Application.ModelCaptureException;
using AutoMapper;
using MediatR;
using Persistence.Dapper_Configuration.Dtos;
using Persistence.Dapper_Configuration.Interfaces;

namespace Application.Commands.Professor_Info_Command
{
    public class CreateProfessorInformation
    {
        public class NewProfessorInfoAsync : IRequest<Unit>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string Grade { get; set; }
        }


        public class CommandHandler : IRequestHandler<NewProfessorInfoAsync,Unit>
        {
            private readonly IProfessorServices _professorServicesInject;
            private readonly IMapper _automapperInject;

            public CommandHandler(IProfessorServices professorServicesInject,
                IMapper automapperInject)
            {
                _professorServicesInject = professorServicesInject;
                _automapperInject = automapperInject;
            }

            public async Task<Unit> Handle(NewProfessorInfoAsync request, CancellationToken cancellationToken)
            {
                var createInformationModel = this._automapperInject.Map<ProfessorDto>(request);
                var Result_To_Operation = await this._professorServicesInject.CreateProfessorInSystem(createInformationModel);
                if (Result_To_Operation > 0)
                {
                    return Unit.Value;
                }

                throw new CaptureExceptions(System.Net.HttpStatusCode.InternalServerError,
                    new {MessageInfo = "Ocurrio un Error!!, No se pudo ingresar la informacion del 'INSTRUCTOR'..."});
            }
        }
    }
}
