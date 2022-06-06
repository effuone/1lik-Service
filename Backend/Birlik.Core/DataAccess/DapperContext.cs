using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Birlik.Core.DataAccess
{
    public class DapperContext 
    { 
        private readonly IConfiguration _configuration; 
        private readonly string _connectionString; 
        public DapperContext(IConfiguration configuration) 
        { 
            _configuration = configuration; _connectionString = _configuration.GetConnectionString("LaptopConnection"); 
        } 
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}