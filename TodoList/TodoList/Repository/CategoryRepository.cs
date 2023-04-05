using Dapper;
using TodoList.Data;
using TodoList.Models;
using Task = System.Threading.Tasks.Task;

namespace TodoList.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly DapperContext _context;

		public CategoryRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Category category)
		{
			string query = "INSERT INTO [dbo].[Categories] (Name) VALUES (@Name)";
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, category);
			}
		}

		public async Task DeleteByIdAsync(int id)
		{
			string query = "DELETE FROM [dbo].[Categories] WHERE Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new { Id = id });
			}
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			string query = "SELECT * FROM [dbo].[Categories]";
			using (var connection = _context.CreateConnection())
			{
				var categories = await connection.QueryAsync<Category>(query);
				return categories.ToList();
			}
		}

		public async Task<Category> GetByIdAsync(int id)
		{
			string query = "SELECT * FROM [dbo].[Categories] WHERE Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				var category = await connection.QueryFirstOrDefaultAsync<Category>(query, new { Id = id });
				return category;
			}
		}

		public async Task UpdateByIdAsync(int id, Category newCategory)
		{
			string query = "UPDATE [dbo].[Categories] SET Name = @Name WHERE Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new 
				{
					Id = id,
					newCategory.Name
				});
			}
		}
	}
}
