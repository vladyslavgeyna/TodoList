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

		public Task AddAsync(Category category)
		{
			throw new NotImplementedException();
		}

		public Task DeleteByIdAsync(int id)
		{
			throw new NotImplementedException();
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

		public Task<Category> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
