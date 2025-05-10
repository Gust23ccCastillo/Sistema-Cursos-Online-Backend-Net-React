using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.DbContextApplication;

namespace Application.Commands
{
    public class CreateCourseInformation
    {
        public class NewInfoCourseAsync : IRequest<Unit>
        {
            public string? TitleParameter { get; set; }

            public string? DescriptionParameter { get; set; }

            public DateTime? DatetimePublicParameter { get; set; }

        }

        public class CommandHandler : IRequestHandler<NewInfoCourseAsync, Unit>
        {
            private readonly CourseOnlineDbContext _courseOnlineDbContextInject;

            public CommandHandler(CourseOnlineDbContext courseOnlineDbContextInject)
            {
                _courseOnlineDbContextInject = courseOnlineDbContextInject;
            }

            public async Task<Unit> Handle(NewInfoCourseAsync request, CancellationToken cancellationToken)
            {
                if (!request.TitleParameter.IsNullOrEmpty() &&
                    !request.DescriptionParameter.IsNullOrEmpty() &&
                    request.DatetimePublicParameter != null)
                {
                    var existingCourse = await this.VerifyExistinCourse(request.TitleParameter);
                    if(existingCourse == false)
                    {
                        var newCourse = new Course
                        {
                            Title = request.TitleParameter,
                            Descriptions = request.DescriptionParameter,
                            DatetimePublic = request.DatetimePublicParameter
                        };

                        _courseOnlineDbContextInject.tb_Course.Add(newCourse);
                        var resultToOperation = await _courseOnlineDbContextInject.SaveChangesAsync(cancellationToken);

                        if (resultToOperation > 0)
                        {
                            return Unit.Value;
                        }
                        throw new Exception("Error en la base de datos");
                    }

                    throw new Exception("El course debe llevar otro titulo ya que existe uno registrado igual!!.");
                }
                else
                {
                    throw new Exception("Los datos debe venir completos!!.");
                }
            }

            private async Task<bool> VerifyExistinCourse(string TitleCourseParameter)
            {
                if (!string.IsNullOrEmpty(TitleCourseParameter))
                {
                    var resultToSearhExistingCourseByTitle = await this._courseOnlineDbContextInject.tb_Course
                        .AnyAsync(searhParameter => searhParameter.Title == TitleCourseParameter);
                    return resultToSearhExistingCourseByTitle;
                }
                return false;
               
            }
        }
    }
}
