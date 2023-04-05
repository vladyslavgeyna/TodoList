using System.ComponentModel.DataAnnotations;
using TodoList.Attributes;

namespace TodoList.ViewModels
{
    public class CreateTaskViewModel
    {
        [MinLength(5, ErrorMessage = "The minimun length should be at least 5 characters.")]
        [Required(ErrorMessage = "The description is required")]
        public string Description { get; set; } = null!;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [LessCurrentDate]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "The category is required. If there is no needed category you can create one.")]
        public int CategoryId { get; set; }
    }
}
