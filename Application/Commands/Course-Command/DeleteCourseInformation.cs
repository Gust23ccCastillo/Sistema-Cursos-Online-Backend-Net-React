using System.Net;
using Application.ModelCaptureException;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Commands.Course_Command
{
    public class DeleteCourseInformation
    {
        public class DeleteInfoCourseAsync : IRequest<Unit>
        {
            public Guid IdCourseParameter {  get; set; }
        }

        public class Handler : IRequestHandler<DeleteInfoCourseAsync,Unit>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContextInject;

            public Handler(CourseOnlineDbContext courseOnlineDbContextInject)
            {
                _courseOnlineDbContextInject = courseOnlineDbContextInject;
            }

            public async Task<Unit> Handle(DeleteInfoCourseAsync request, CancellationToken cancellationToken)
            {
                var searhSpecificCourseById = await this._courseOnlineDbContextInject
                    .tb_Course.Where(searchCourse => searchCourse.IdCourse == request.IdCourseParameter)
                    .FirstAsync(cancellationToken);
                if(searhSpecificCourseById == null)
                {
                    //throw new Exception("El course no fue encontrado para eliminarlo!!");
                    throw new CaptureExceptions(HttpStatusCode.NotFound, new {messageInformation = 
                        "El curso no fue encontrado para eliminarlo!!" });
                }

                this._courseOnlineDbContextInject.Remove(searhSpecificCourseById);
                var resultToOperation = await this._courseOnlineDbContextInject.SaveChangesAsync(cancellationToken);
                
                if(resultToOperation > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Problemas para guardar los cambios!!.");

            }
        }

    }
}
