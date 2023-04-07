using TodoList.Models;
using TodoList.Repository;
using TodoList.ViewModels;

namespace TodoList.Utils
{
    public static class IndexTaskViewModelCreator
    {
        public static async Task<IndexTaskViewModel> CreateIndexTaskViewModel(ICategoryRepository categoryRepository, ITaskRepository taskRepository)
        {
            var categories = await categoryRepository.GetAllAsync();
            var tasks = await taskRepository.GetAllAsync();
            var indexTaskViewModel = new IndexTaskViewModel
            {
                Categories = categories.Where(category => category.Tasks.All(task => task is not null)).ToList(),
            };
            var tasksWithoutCategory = tasks.Where(task => task.CategoryId is null).ToList();
            if (tasksWithoutCategory.Any())
            {
                var category = new Category
                {
                    Id = -1,
                    Name = "Without category",
                    Tasks = tasksWithoutCategory.ToList()
                };
                indexTaskViewModel.Categories.Add(category);
            }
            foreach (var category in categories)
            {
                indexTaskViewModel.CategoriesSelect.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }
            indexTaskViewModel.Categories.ForEach(category => category.Tasks = category.Tasks.OrderBy(task => task.IsCompleted).ThenByDescending(task => task.DueDate).ToList());
            return indexTaskViewModel;
        }
    }
}