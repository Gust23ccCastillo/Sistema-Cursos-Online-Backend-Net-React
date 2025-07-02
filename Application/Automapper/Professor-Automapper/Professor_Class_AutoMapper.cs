using AutoMapper;
using Persistence.Dapper_Configuration.Dtos;
using static Application.Commands.Professor_Info_Command.CreateProfessorInformation;
using static Application.Commands.Professor_Info_Command.EditProfessorInformation;

namespace Application.Automapper.Professor_Automapper
{
    public class Professor_Class_AutoMapper:Profile
    {
        public Professor_Class_AutoMapper()
        {
            //continuar aqui
            CreateMap<NewProfessorInfoAsync, ProfessorDto>();
            CreateMap<UpdateInformationAsync, ProfessorDto>();
                
        }
    }
}
