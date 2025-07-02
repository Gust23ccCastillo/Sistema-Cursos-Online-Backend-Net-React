using MediatR;
using Persistence.Dapper_Configuration.Dtos;
using Persistence.Dapper_Configuration.Interfaces;

namespace Application.Queries.Professor_Queries
{
    public class GetListProfessorInSystem
    {
        public class GetListAsync : IRequest<List<ProfessorDto>> { }

        public class QueryHandler : IRequestHandler<GetListAsync, List<ProfessorDto>>
        {
            //SERVICIO DE INJECCION CON UTILIZACON DE DAPPER DESDE PERSISTENCIA
            private readonly IProfessorServices _professorServicesInject;

            public QueryHandler(IProfessorServices professorServicesInject)
            {
                _professorServicesInject = professorServicesInject;
            }

            public async Task<List<ProfessorDto>> Handle(GetListAsync request, CancellationToken cancellationToken)
            {
               var ListProfessor = await this._professorServicesInject.GetAllProfessorInSystem();
                return ListProfessor.ToList();
            }
        }
    }
}
