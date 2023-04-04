namespace TodoList.Repository
{
	public interface ITaskRepository
	{
		Task<IEnumerable<Models.Task>> GetAllAsync();
		Task<Models.Task> GetByIdAsync(int id);
		Task AddAsync(Models.Task task);
		Task DeleteByIdAsync(int id);
		Task UpdateByIdAsync(int id);
	}
}
