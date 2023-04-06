using Microsoft.Data.SqlClient;
using System.Data;
using TodoList.Extensions;

namespace TodoList.Data
{
	public sealed class DapperContext
	{
		private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
			_connectionString = configuration.GetDefaultConnectionString() 
			                    ?? throw new Exception("Cannot get connection string");
		}
		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
    }
}
