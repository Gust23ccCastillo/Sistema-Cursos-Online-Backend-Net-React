using Application.Automapper.Courses.Query;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Queries.Course_Queries
{
    public class GetUniqueCourseByParameter
    {
        public class GetOneCourseByIdParameterAsync : IRequest<CourseInformationDto>
        {
            public Guid IdParameter { get; set; }
        }

        public class QueryHandler : IRequestHandler<GetOneCourseByIdParameterAsync, CourseInformationDto>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContext;
            private readonly IMapper _AutomapperInject;

            public QueryHandler(CourseOnlineDbContext courseOnlineDbContext, 
                IMapper automapperInject)
            {
                _courseOnlineDbContext = courseOnlineDbContext;
                _AutomapperInject = automapperInject;
            }

            public async Task<CourseInformationDto> Handle(GetOneCourseByIdParameterAsync request, CancellationToken cancellationToken)
            {
                var searchCourseByIdParameter = await this._courseOnlineDbContext
                     .tb_Course
                     .AsNoTracking()
                     .Include(includeTable => includeTable.ProfessorLink) 
                     .ThenInclude(property => property.Professor)
                     .Include(IncludeTable => IncludeTable.AsignedComments)
                     .Include(IncludeTable => IncludeTable.PriceAsigned)
                     .Where(parameter=> parameter.IdCourse == request.IdParameter)
                     .FirstAsync(cancellationToken);

                var returnCourseInformation = this._AutomapperInject.Map<CourseInformationDto>(searchCourseByIdParameter);

                return returnCourseInformation;
            }
        }
    }
}
