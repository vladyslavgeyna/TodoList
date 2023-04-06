using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class CreateCategoryViewModel
    {
        [MinLength(3, ErrorMessage = "The minimum length should be at least 3 characters.")]
        [Required(ErrorMessage = "The category name is required.")]
        public string Name { get; set; } = null!;
    }
}
