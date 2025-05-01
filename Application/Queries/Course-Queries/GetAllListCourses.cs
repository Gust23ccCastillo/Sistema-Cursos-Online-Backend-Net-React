using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Queries.Course_Queries
{
    public class GetAllListCourses
    {
        public class AllListCourseAsync : IRequest<List<Course>> { }

        public class QueryHandler : IRequestHandler<AllListCourseAsync, List<Course>>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContext;

            public QueryHandler(CourseOnlineDbContext courseOnlineDbContext)
            {
                _courseOnlineDbContext = courseOnlineDbContext;
            }

            public async Task<List<Course>> Handle(AllListCourseAsync request, CancellationToken cancellationToken)
            {
                var allListCourses = await _courseOnlineDbContext.tb_Course
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return allListCourses;
            }
        }
    }
}
