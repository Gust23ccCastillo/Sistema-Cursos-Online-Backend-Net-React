using FluentValidation;
using static Application.Commands.UpdateCourseInformation;

namespace Application.Automapper.Courses.Command
{
    public class UpdateCourseValidatorDto : AbstractValidator<UpdateInformationForCourseAsync>
    {
        public UpdateCourseValidatorDto()
        {
            RuleFor(parameter => parameter.TitleParameter)
               .NotEmpty().WithMessage("Debe ingresar un titulo al curso!!.");

            RuleFor(parameter => parameter.DescriptionParameter)
                .NotEmpty().WithMessage("Debe ingresar una descripcion al curso!!.");

            RuleFor(parameter => parameter.DatetimePublicParameter)
                .NotEmpty().WithMessage("Debe ingresar una fecha de publicacion del curso!!.");
        }
    }
}
