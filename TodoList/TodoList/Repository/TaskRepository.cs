using Dapper;
using TodoList.Data;
using TodoList.Models;
using Task = System.Threading.Tasks.Task;

namespace TodoList.Repository
{
	public class TaskRepository : ITaskRepository
	{
		private readonly DapperContext _context;

		public TaskRepository(DapperContext context)
		{
			_context = context;
		}

        public async Task AddAsync(Models.Task task)
		{
			string query = "INSERT INTO [dbo].[Tasks] (Description, DueDate, DateOfCreation, CategoryId) VALUES (@Description, @DueDate, @DateOfCreation, @CategoryId)";
			using (var connection = _context.CreateConnection())
			{
                task.DateOfCreation = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				await connection.ExecuteAsync(query, task);
			}
		}

		public async Task DeleteByIdAsync(int id)
		{
			string query = "DELETE FROM [dbo].[Tasks] WHERE Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new { Id = id });
			}
		}

		public async Task<IEnumerable<Models.Task>> GetAllAsync()
		{
			string query = "SELECT * FROM [dbo].[Tasks] LEFT JOIN [dbo].[Categories] ON [dbo].[Tasks].CategoryId = [dbo].[Categories].Id";
			using (var connection = _context.CreateConnection())
			{
				var tasks = await connection.QueryAsync<Models.Task, Category, Models.Task>(query, (task, category) =>
				{
					task.Category = category;
					task.CategoryId = category.Id == 0 ? null : category.Id;
					return task;
				},
				splitOn: "CategoryId");
				return tasks.ToList();
			}
		}

		public async Task<Models.Task> GetByIdAsync(int id)
		{
			string query = "SELECT * FROM [dbo].[Tasks] WHERE Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				var task = await connection.QueryFirstOrDefaultAsync<Models.Task>(query, new { Id = id });
				return task;
			}
		}

		public async Task UpdateByIdAsync(int id, Models.Task newTask)
		{
			string query = "UPDATE [dbo].[Tasks] SET Description = @Description, DueDate = @DueDate, IsCompleted = @IsCompleted, CategoryId = @CategoryId WHERE Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new 
				{
					Id = id,
					newTask.Description,
					newTask.DueDate,
					newTask.IsCompleted,
					newTask.CategoryId
				});
			}
		}
	}
}
