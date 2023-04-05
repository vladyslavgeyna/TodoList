using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TodoList.Attributes;
using TodoList.Models;

namespace TodoList.ViewModels
{
	public class IndexTaskViewModel
	{
		public IEnumerable<Category> Categories { get; set; } = null!;
		//[MinLength(5, ErrorMessage = "The minimun length should be at least 5 characters.")]
        //[Required(ErrorMessage = "The description is required")]
        //public string Description { get; set; } = null!;
		//[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
		//[LessDate]
        //public DateTime DueDate { get; set; }
		//[Required(ErrorMessage = "The category is required. If there is no needed category you can create one.")]
		//public int CategoryId { get; set; }
		public ICollection<SelectListItem> CategoriesSelect { get; set; } = new List<SelectListItem>();
        //[MinLength(3, ErrorMessage = "The minimun length should be at least 3 characters.")]
        //[Required(ErrorMessage = "The category name is required")]
        //public string Name { get; set; } = null!;

		public CreateTaskViewModel CreateTaskViewModel { get; set; } = null!;
		public CreateCategoryViewModel CreateCategoryViewModel { get; set; } = null!;
    }
}
