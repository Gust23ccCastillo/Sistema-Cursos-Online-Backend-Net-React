using System.Data;
using Dapper;
using Persistence.Dapper_Configuration.Dtos;
using Persistence.Dapper_Configuration.Interfaces;

namespace Persistence.Dapper_Configuration.Repositories
{
    public class ProfessorRepository : IProfessorServices
    {
        private readonly IFactoryConnection _factoryConnection;

        public ProfessorRepository(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }

        public async Task<IEnumerable<ProfessorDto>> GetAllProfessorInSystem()
        {
            IEnumerable<ProfessorDto> allProfessorList = null;
            var storeProcedure = "sp_Get_All_Professor";
            try
            {
                var executeConnection = _factoryConnection.GetConnection();
                allProfessorList = await executeConnection.QueryAsync<ProfessorDto>(storeProcedure, null,
                    commandType: CommandType.StoredProcedure);

            }
            catch (Exception captureException)
            {
                throw new Exception("Error!, Problemas en capturar los datos..",captureException);
            }
            finally
            {
                _factoryConnection.CloseConnectionExisting();
            }

            return allProfessorList;
        }

        public async Task<ProfessorDto> GetProfessorById(Guid IdParameter)
        {
            var storeProcedureName = "sp_get_professor_byId";
            ProfessorDto professorInformation = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                professorInformation = await connection.QueryFirstAsync<ProfessorDto>(
                    storeProcedureName,
                    new
                    {
                        IdParameter = IdParameter
                    },
                    commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnectionExisting();
                return professorInformation;
            }
            catch (Exception captureException)
            {
                throw new Exception("Error!!, No se encontro el 'INSTRUCTOR' en el systema", captureException);
            }
        }
        public async Task<int> CreateProfessorInSystem(ProfessorDto professorDtoParameter)
        {
            var nameStoreProcedure = "sp_new_professor_system";
            try
            {
                var connectionState = _factoryConnection.GetConnection();
                var Transaction_Result_To_Operation = await connectionState.ExecuteAsync(
                 nameStoreProcedure, 
                    new
                    {
                        IdProfessor = Guid.NewGuid(),
                        Name = professorDtoParameter.FirstName,
                        LastName = professorDtoParameter.LastName,
                        Grade = professorDtoParameter.Grade
                    },
                    commandType:CommandType.StoredProcedure);
                _factoryConnection.CloseConnectionExisting();
                return Transaction_Result_To_Operation;
            }
            catch (Exception captureException)
            {
                throw new Exception("Ocurrio un Error!!, No se pudo almacenar los nuevos datos del instructor!!.", captureException);
            }
        }

        public Task<int> UpdateProfessorInSystem(ProfessorDto professorDtoParameter)
        {
            var nameStoreProcedure = "sp_edit_professor_system";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var Transaction_Result_To_Operation = connection.ExecuteAsync(
                        nameStoreProcedure,
                        new
                        {
                            IdProfessor = professorDtoParameter.ProfessorId,
                            Name = professorDtoParameter.FirstName,
                            LastName = professorDtoParameter.LastName,
                            Grade = professorDtoParameter.Grade
                        },
                        commandType: CommandType.StoredProcedure
                        );

                _factoryConnection.CloseConnectionExisting();
                return Transaction_Result_To_Operation;
            }
            catch (Exception captureException)
            {

                throw new Exception("Ocurrio un Error!!, No se pudo actualizar la informacion del instructor.",captureException);
            }
        }
        public async Task<int> DeleteProfessorById(Guid IdParameter)
        {
            var nameStoreProcedure = "sp_remove_professor_system";
            try
            {
                var connection = this._factoryConnection.GetConnection();
                var Transaction_Result_To_Operation = await connection.ExecuteAsync(
                    nameStoreProcedure,
                    new
                    {
                        IdProfessor = IdParameter,
                    },
                    commandType: CommandType.StoredProcedure);
                _factoryConnection.CloseConnectionExisting();
                return Transaction_Result_To_Operation;
            }
            catch (Exception captureException)
            {

                throw new Exception("Error!!, Ocurrio un problema con la eliminacion de la informacion.", captureException);
            }
        }
    }
}
