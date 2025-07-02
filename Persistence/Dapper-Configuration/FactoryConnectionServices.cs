using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Persistence.Dapper_Configuration.Connection;

namespace Persistence.Dapper_Configuration
{
    public class FactoryConnectionServices : IFactoryConnection
    {
        private IDbConnection _dbconnection;
        private readonly IOptions<DapperConfigInformation> _dapperConnectionSQLConfig;
        public FactoryConnectionServices(IOptions<DapperConfigInformation> configs) 
        {
            _dapperConnectionSQLConfig = configs;
        
        }

        //CERRAR CONEXION CON DAPPER
        public void CloseConnectionExisting()
        {
            if (_dbconnection != null && _dbconnection.State == ConnectionState.Open)
            {
                _dbconnection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            //EVALUA PARA CREAR LA CONEXION DE DAPPER CON SQL 
            if (_dbconnection == null)
            {
                _dbconnection = new SqlConnection(_dapperConnectionSQLConfig.Value.StringKey);
            }
            //ABRO O ACTIVO EL ENLACE ENTRE DAPPER Y SQL 
            if(_dbconnection.State != ConnectionState.Open)
            {
                _dbconnection.Open();
            }
            return _dbconnection;
        }
    }
}
