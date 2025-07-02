using System.Data;

namespace Persistence.Dapper_Configuration
{
    public interface IFactoryConnection
    {
        void CloseConnectionExisting();
        IDbConnection GetConnection();
    }
}
