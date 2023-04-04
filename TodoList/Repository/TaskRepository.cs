using Dapper;
using TodoList.Data;

namespace TodoList.Repository
{
	public class TaskRepository : ITaskRepository
	{
		private readonly DapperContext _context;

		public TaskRepository(DapperContext context)
		{
			_context = context;
		}

        public Task AddAsync(Models.Task task)
		{
			throw new NotImplementedException();
		}

		public Task DeleteByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Models.Task>> GetAllAsync()
		{
			string query = "SELECT * FROM [dbo].[Tasks]";
			using (var connection = _context.CreateConnection())
			{
				var tasks = await connection.QueryAsync<Models.Task>(query);
				return tasks.ToList();
			}
		}

		public Task<Models.Task> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
