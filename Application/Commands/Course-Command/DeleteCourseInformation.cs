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
                //Eliminar Primero Profesores Asignados al Course Antes de Eliminar el Curso
                var instructorsAsinedCourse = await this._courseOnlineDbContextInject
                    .tb_Course_Professor.Where(search => search.IdCourse == request.IdCourseParameter).ToListAsync();

                foreach (var id_Instructor_Remove in instructorsAsinedCourse)
                {
                    this._courseOnlineDbContextInject.Remove(id_Instructor_Remove);
                }

                //Obtener la lista de comentarios para eliminarlos
                var obtainCommentaryList = await this._courseOnlineDbContextInject.tb_Comments
                    .Where(search => search.IdCourse == request.IdCourseParameter).ToListAsync();
                foreach(var itemCommentary in obtainCommentaryList)
                {
                    _courseOnlineDbContextInject.tb_Comments.Remove(itemCommentary);
                }


                //Eliminar precio del curso de la tabla precio
                var priceofCourseSpecific =  await this._courseOnlineDbContextInject.tb_Price
                    .Where(search => search.IdCourse == request.IdCourseParameter).FirstOrDefaultAsync();

                if (priceofCourseSpecific != null)
                {
                    this._courseOnlineDbContextInject.Remove(priceofCourseSpecific);
                }




                var searhSpecificCourseById = await this._courseOnlineDbContextInject
                    .tb_Course.Where(searchCourse => searchCourse.IdCourse == request.IdCourseParameter)
                    .FirstAsync(cancellationToken);
                if(searhSpecificCourseById == null)
                {
                  
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
