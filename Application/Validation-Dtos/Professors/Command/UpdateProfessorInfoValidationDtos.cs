using FluentValidation;
using static Application.Commands.Professor_Info_Command.EditProfessorInformation;

namespace Application.Validation_Dtos.Professors.Command
{
    public class UpdateProfessorInfoValidationDtos:AbstractValidator<UpdateInformationAsync>
    {
        public UpdateProfessorInfoValidationDtos()
        {
            RuleFor(parameter => parameter.IdProfessor)
                .NotEmpty().WithMessage("Debe ingresar el identificador del Professor");

            RuleFor(parameter => parameter.FirstName)
           .NotEmpty().WithMessage("Debe Ingresar un Nombre!.")
           .MaximumLength(500).WithMessage("El maximo de caracteres superado!!")
           .MinimumLength(4).WithMessage("Debes Ingresar un nombre minimo de '4' caracteres..");

            RuleFor(parameter => parameter.LastName)
            .NotEmpty().WithMessage("Debe Ingresar un Apellido!.")
            .MaximumLength(500).WithMessage("El maximo de caracteres superado!!")
            .MinimumLength(4).WithMessage("Debes Ingresar un Apellido minimo de '4' caracteres..");

            RuleFor(parameter => parameter.Grade)
            .NotEmpty().WithMessage("Debe Ingresar un Grado Academico!.")
            .MaximumLength(500).WithMessage("El maximo de caracteres superado!!")
            .MinimumLength(4).WithMessage("Debes Ingresar un Grado Academico minimo de '4' caracteres..");
        }
    }
}
