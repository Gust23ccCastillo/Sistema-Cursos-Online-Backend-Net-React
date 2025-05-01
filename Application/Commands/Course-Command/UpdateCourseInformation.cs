using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Commands
{
    public class UpdateCourseInformation
    {
        public class UpdateInformationForCourseAsync : IRequest<Course>
        {
            public int IdCourseParameter { get; set; }

            public string TitleParameter { get; set; }

            public string DescriptionParameter { get; set; }

            public DateTime? DatetimePublicParameter { get; set; }
        }

        public class Handler : IRequestHandler<UpdateInformationForCourseAsync, Course>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContextInject;

            public Handler(CourseOnlineDbContext courseOnlineDbContextInject)
            {
                _courseOnlineDbContextInject = courseOnlineDbContextInject;
            }

            public async Task<Course> Handle(UpdateInformationForCourseAsync request, CancellationToken cancellationToken)
            {
                var searchCourseSpecificById = await _courseOnlineDbContextInject
                    .tb_Course.Where(searhCourse => searhCourse.IdCourse == request.IdCourseParameter)
                    .FirstAsync(cancellationToken);

                if (searchCourseSpecificById == null)
                {
                    throw new Exception("El curso a buscar la informacion para actualizar no existe!");
                }

                searchCourseSpecificById.Title = request.TitleParameter ?? searchCourseSpecificById.Title;
                searchCourseSpecificById.Descriptions = request.DescriptionParameter ?? searchCourseSpecificById.Descriptions;
                searchCourseSpecificById.DatetimePublic = request.DatetimePublicParameter ?? searchCourseSpecificById.DatetimePublic;

                this._courseOnlineDbContextInject.tb_Course.Update(searchCourseSpecificById);
                var resultToOperation = await _courseOnlineDbContextInject.SaveChangesAsync(cancellationToken);

                if (resultToOperation > 0)
                {
                    return searchCourseSpecificById;
                }

                throw new Exception("Problemas para actualizar la informacion de cursos!");
            }
        }
    }
}
