using Application.Automapper.Courses.Query;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Queries.Course_Queries
{
    public class GetAllListCourses
    {
        public class AllListCourseAsync : IRequest<List<CourseInformationDto>> { }

        public class QueryHandler : IRequestHandler<AllListCourseAsync, List<CourseInformationDto>>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContext;
            private readonly IMapper _InjectAutomapper;

            public QueryHandler(CourseOnlineDbContext courseOnlineDbContext, 
                IMapper injectAutomapper)
            {
                _courseOnlineDbContext = courseOnlineDbContext;
                _InjectAutomapper = injectAutomapper;
            }

            public async Task<List<CourseInformationDto>> Handle(AllListCourseAsync request, CancellationToken cancellationToken)
            {
                var allListCourses = await _courseOnlineDbContext.tb_Course
                     .AsNoTracking()
                    .Include(IncludeTable  => IncludeTable.ProfessorLink)
                    .ThenInclude(information => information.Professor)
                    .Include(IncludeTable => IncludeTable.AsignedComments)
                    .Include(IncludeTable => IncludeTable.PriceAsigned)
                    .ToListAsync(cancellationToken);

                var returnListCourseAndProfessors = this._InjectAutomapper.Map<List<CourseInformationDto>>(allListCourses);

                return returnListCourseAndProfessors;
            }
        }
    }
}
