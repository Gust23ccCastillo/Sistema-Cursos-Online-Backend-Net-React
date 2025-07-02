using System.Net;
using Application.ModelCaptureException;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContextApplication;

namespace Application.Commands
{
    public class UpdateCourseInformation
    {
        public class UpdateInformationForCourseAsync : IRequest<Unit>
        {
            public Guid IdCourseParameter { get; set; }

            public string TitleParameter { get; set; }

            public string DescriptionParameter { get; set; }

            public DateTime? DatetimePublicParameter { get; set; }

            public List<Guid> ListProfessors {  get; set; }

            public decimal? Price { get; set; }

            public decimal? OfferPrice { get; set; }
        }

        public class Handler : IRequestHandler<UpdateInformationForCourseAsync,Unit>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContextInject;

            public Handler(CourseOnlineDbContext courseOnlineDbContextInject)
            {
                _courseOnlineDbContextInject = courseOnlineDbContextInject;
            }

            public async Task<Unit> Handle(UpdateInformationForCourseAsync request, CancellationToken cancellationToken)
            {
                var searchCourseSpecificById = await _courseOnlineDbContextInject
                    .tb_Course.Where(searhCourse => searhCourse.IdCourse == request.IdCourseParameter)
                    .FirstAsync(cancellationToken);

                if (searchCourseSpecificById == null)
                {
                    throw new CaptureExceptions(HttpStatusCode.NotFound, new { 
                        messageInformation = "El curso a buscar la informacion para actualizar no existe!."
                    });
                }

                searchCourseSpecificById.Title = request.TitleParameter ?? searchCourseSpecificById.Title;
                searchCourseSpecificById.Descriptions = request.DescriptionParameter ?? searchCourseSpecificById.Descriptions;
                searchCourseSpecificById.DatetimePublic = request.DatetimePublicParameter ?? searchCourseSpecificById.DatetimePublic;
                this._courseOnlineDbContextInject.tb_Course.Update(searchCourseSpecificById);

                //Logica para actualizar el precio del curso
                var ObtainPriceEntity = _courseOnlineDbContextInject.tb_Price
                    .Where(searchParameter => searchParameter.IdCourse == searchCourseSpecificById.IdCourse)
                    .FirstOrDefault();

                if(ObtainPriceEntity != null)
                {
                    ObtainPriceEntity.PromotionalPrice = request.OfferPrice ?? ObtainPriceEntity.PromotionalPrice;
                    ObtainPriceEntity.CurrentPrice = request.Price ?? ObtainPriceEntity.CurrentPrice;
                }
                else
                {
                    ObtainPriceEntity = new Price
                    {
                        IdPrice = Guid.NewGuid(),
                        PromotionalPrice = request.OfferPrice??0,
                        CurrentPrice = request.Price ?? 0,
                        IdCourse = searchCourseSpecificById.IdCourse
                    };

                    await _courseOnlineDbContextInject
                        .tb_Price.AddAsync(ObtainPriceEntity);
                }



                //Parte para Actualizar la lista de instructores
                if (request.ListProfessors != null)
                {
                    if (request.ListProfessors.Count() > 0)
                    {
                        /*Eliminar Profesores actuales del curso*/
                        var getProfessoresInDatabase = await this._courseOnlineDbContextInject.tb_Course_Professor
                            .Where(search => search.IdCourse == request.IdCourseParameter).ToListAsync();
                        if(getProfessoresInDatabase.Count() > 0)
                        {
                            foreach (var id_Instructor_Remove in getProfessoresInDatabase)
                            {
                                this._courseOnlineDbContextInject.tb_Course_Professor.Remove(id_Instructor_Remove);
                            }
                            /*Fin del procedimiento para eliminar Profesores del curso*/
                        }

                        /*Proceso para agregar el nuevo Profesor de un curso*/
                        foreach (var id_Instructor_Agregated in request.ListProfessors)
                        {
                            var agregatedNewInstructor = new Course_Professor
                            {
                                IdCourse = request.IdCourseParameter,
                                IdProfessor = id_Instructor_Agregated,
                            };

                            this._courseOnlineDbContextInject.tb_Course_Professor.Add(agregatedNewInstructor);
                        }
                    }
                }
                var resultToOperation = await _courseOnlineDbContextInject.SaveChangesAsync(cancellationToken);
                if (resultToOperation > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Problemas para actualizar la informacion de cursos!");
            }
        }
    }
}
