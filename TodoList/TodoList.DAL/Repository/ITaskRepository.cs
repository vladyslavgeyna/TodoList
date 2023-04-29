using Task = System.Threading.Tasks.Task;

namespace TodoList.DAL.Repository
{
	public interface ITaskRepository
	{
		Task<IEnumerable<Domain.Entity.Task>> GetAllAsync();
		Task<Domain.Entity.Task?> GetByIdAsync(int id);
		Task AddAsync(Domain.Entity.Task task);
		Task DeleteByIdAsync(int id);
        Task UpdateByIdAsync(int id, Domain.Entity.Task newTask);
	}
}
