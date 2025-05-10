using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Queries.Course_Queries
{
    public class GetUniqueCourseByParameter
    {
        public class GetOneCourseByIdParameterAsync : IRequest<Course>
        {
            public Guid IdParameter { get; set; }
        }

        public class QueryHandler : IRequestHandler<GetOneCourseByIdParameterAsync, Course>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContext;

            public QueryHandler(CourseOnlineDbContext courseOnlineDbContext)
            {
                _courseOnlineDbContext = courseOnlineDbContext;
            }

            public async Task<Course> Handle(GetOneCourseByIdParameterAsync request, CancellationToken cancellationToken)
            {
                var searchCourseByIdParameter = await this._courseOnlineDbContext
                     .tb_Course.Where(parameter=> parameter.IdCourse == request.IdParameter)
                     .FirstAsync(cancellationToken);

                return searchCourseByIdParameter;
            }
        }
    }
}
