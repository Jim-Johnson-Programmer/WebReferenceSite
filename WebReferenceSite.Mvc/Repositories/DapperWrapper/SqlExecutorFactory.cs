using System;
using System.Data.SqlClient;

namespace WebReferenceSite.Mvc.Repositories.DapperWrapper
{ 
    public class SqlExecutorFactory : IDbExecutorFactory
    {
        readonly string _connectionString;
        
        public SqlExecutorFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");
            _connectionString = connectionString;
        }
        
        public IDbExecutor CreateExecutor()
        {
            var dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return new SqlExecutor(dbConnection);
        }
    }
}