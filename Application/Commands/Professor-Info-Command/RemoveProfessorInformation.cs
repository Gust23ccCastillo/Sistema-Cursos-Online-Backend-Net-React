using Application.ModelCaptureException;
using MediatR;
using Persistence.Dapper_Configuration.Interfaces;

namespace Application.Commands.Professor_Info_Command
{
    public class RemoveProfessorInformation
    {
        public class DeleteInformationAsync : IRequest<Unit>
        {
            public Guid IdProfessor { get; set; }
        }

        public class CommandHanlder : IRequestHandler<DeleteInformationAsync, Unit>
        {
            private readonly IProfessorServices _professorServicesInject;

            public CommandHanlder(IProfessorServices professorServicesInject)
            {
                _professorServicesInject = professorServicesInject;
            }

            public async Task<Unit> Handle(DeleteInformationAsync request, CancellationToken cancellationToken)
            {
                var Result_To_Operation = await this._professorServicesInject.DeleteProfessorById(request.IdProfessor);
                if (Result_To_Operation > 0)
                {
                    return Unit.Value;
                }

                throw new CaptureExceptions(System.Net.HttpStatusCode.InternalServerError,
                    new {messageInformation = "Ocurrio un Error!, No se pudo eliminar la infomracion del 'PROFESOR'."});
            }
        }
    }
}
