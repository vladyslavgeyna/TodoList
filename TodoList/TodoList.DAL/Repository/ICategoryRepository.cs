using TodoList.Domain.Entity;
using Task = System.Threading.Tasks.Task;

namespace TodoList.DAL.Repository
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
		Task DeleteByIdAsync(int id);
		Task UpdateByIdAsync(int id, Category newCategory);
	}
}
