using Dapper;
using TodoList.Data;
using TodoList.Models;
using Task = System.Threading.Tasks.Task;

namespace TodoList.Repository
{
	public class CategoryMsSqlRepository : ICategoryRepository
	{
		private readonly DapperContext _context;

		public CategoryMsSqlRepository(DapperContext context)
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
			string query = "SELECT * FROM [dbo].[Categories] LEFT JOIN [dbo].[Tasks] ON [dbo].[Categories].Id = [dbo].[Tasks].CategoryId";
			using (var connection = _context.CreateConnection())
			{
				var categoryDick = new Dictionary<int, Category>();
				var categories = await connection.QueryAsync<Category, Models.Task, Category>(query, (category, task) =>
				{
					if (!categoryDick.TryGetValue(category.Id, out var currentCategory))
					{
						currentCategory = category;
						categoryDick.Add(currentCategory.Id, currentCategory);
					}
					currentCategory.Tasks.Add(task);
					return currentCategory;
				},
				splitOn: "Id");
				return categories.Distinct().ToList();
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
