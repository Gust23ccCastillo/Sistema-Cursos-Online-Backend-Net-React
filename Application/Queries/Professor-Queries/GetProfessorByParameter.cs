using Application.ModelCaptureException;
using MediatR;
using Persistence.Dapper_Configuration.Dtos;
using Persistence.Dapper_Configuration.Interfaces;

namespace Application.Queries.Professor_Queries
{
    public class GetProfessorByParameter
    {
        public class GetInformationById : IRequest<ProfessorDto>
        {
            public Guid IdParameter { get; set; }
        }

        public class QueryHandler : IRequestHandler<GetInformationById,ProfessorDto>
        {
            private readonly IProfessorServices _professorServicesInject;

            public QueryHandler(IProfessorServices professorServicesInject)
            {
                _professorServicesInject = professorServicesInject;
            }

            public async Task<ProfessorDto> Handle(GetInformationById request, CancellationToken cancellationToken)
            {
                var getInformationByProfessor = await this._professorServicesInject.GetProfessorById(request.IdParameter);
                if (getInformationByProfessor == null)
                {
                    throw new CaptureExceptions(System.Net.HttpStatusCode.NotFound,
                        new {messageInformation = "Profesor no registrado en el sistema.."});
                }
                return getInformationByProfessor;
            }
        }
    }
}
