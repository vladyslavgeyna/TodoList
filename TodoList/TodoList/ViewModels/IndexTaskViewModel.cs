using Microsoft.AspNetCore.Mvc.Rendering;
using TodoList.Models;

namespace TodoList.ViewModels
{
	public class IndexTaskViewModel
	{
		public List<Category> Categories { get; set; } = null!;
		public List<Models.Task> Tasks { get; set; } = null!;
		public ICollection<SelectListItem> CategoriesSelect { get; set; } = new List<SelectListItem>();
		public CreateTaskViewModel CreateTaskViewModel { get; set; } = null!;
		public CreateCategoryViewModel CreateCategoryViewModel { get; set; } = null!;
		public EditIsCompletedTaskViewModel EditIsCompletedTaskViewModel { get; set; } = null!;
		public DeleteTaskViewModel DeleteTaskViewModel { get; set; } = null!;
		public DeleteCategoryViewModel DeleteCategoryViewModel { get; set; } = null!;
    }
}
