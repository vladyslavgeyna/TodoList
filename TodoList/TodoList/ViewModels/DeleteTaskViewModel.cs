using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
    public class DeleteTaskViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
