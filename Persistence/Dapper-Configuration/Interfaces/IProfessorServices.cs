using Persistence.Dapper_Configuration.Dtos;

namespace Persistence.Dapper_Configuration.Interfaces
{
    public interface IProfessorServices
    {
        Task<IEnumerable<ProfessorDto>> GetAllProfessorInSystem();
        Task<ProfessorDto> GetProfessorById(Guid IdParameter);

        Task<int>CreateProfessorInSystem(ProfessorDto professorDtoParameter);
        Task<int> UpdateProfessorInSystem(ProfessorDto professorDtoParameter);
        Task<int> DeleteProfessorById(Guid IdParameter);
    }
}
