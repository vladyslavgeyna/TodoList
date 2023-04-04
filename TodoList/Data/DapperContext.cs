using Microsoft.Data.SqlClient;
using System.Data;
using TodoList.Extensions;

namespace TodoList.Data
{
	public sealed class DapperContext
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
			_configuration = configuration;
			_connectionString = _configuration.GetDefaultConnectionString() 
				?? throw new Exception("Cannot get connection string");
		}
		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
    }
}
