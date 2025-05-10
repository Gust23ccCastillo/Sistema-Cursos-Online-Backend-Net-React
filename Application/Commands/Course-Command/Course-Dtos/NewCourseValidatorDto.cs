using FluentValidation;
using static Application.Commands.CreateCourseInformation;

namespace Application.Commands.Course_Command.Course_Dtos
{
    public class NewCourseValidatorDto:AbstractValidator<NewInfoCourseAsync>
    {
        public NewCourseValidatorDto()
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
