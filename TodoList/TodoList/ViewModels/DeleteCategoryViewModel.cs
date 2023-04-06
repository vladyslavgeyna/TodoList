using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class DeleteCategoryViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
