using System.ComponentModel.DataAnnotations;
using TodoList.Attributes;
using TodoList.Service.Utils;

namespace TodoList.ViewModels
{
    public class CreateTaskViewModel
    {
        [MinLength(5, ErrorMessage = "The minimum length should be at least 5 characters.")]
        [Required(ErrorMessage = "The description is required")]
        public string Description { get; set; } = null!;
        [LessCurrentDate]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = $"{{0:{DateTimeHelper.DatePattern}}}")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "The category is required. If there is no needed category you can create one.")]
        [Range(1, int.MaxValue, ErrorMessage = "Incorrect category was chosen.")]
        public int CategoryId { get; set; }
    }
}
