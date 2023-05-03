using TodoList.DAL.Repository;
using TodoList.ViewModels;

namespace TodoList.Factory
{
    public static class IndexTaskViewModelFactory
    {
        public static async Task<IndexTaskViewModel> CreateIndexTaskViewModel(ICategoryRepository categoryRepository, ITaskRepository taskRepository)
        {
            var categories = (await categoryRepository.GetAllAsync()).ToList();
            var tasks = (await taskRepository.GetAllAsync()).ToList();
            var indexTaskViewModel = new IndexTaskViewModel
            {
                Categories = categories,
                Tasks = tasks,
            };
            foreach (var category in categories)
            {
                indexTaskViewModel.CategoriesSelect.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }
            indexTaskViewModel.Tasks = indexTaskViewModel.Tasks.OrderBy(task => task.IsCompleted).ThenByDescending(task => task.DueDate).ToList();
            return indexTaskViewModel;
        }
    }
}