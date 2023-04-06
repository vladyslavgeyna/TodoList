using System.ComponentModel.DataAnnotations;

namespace TodoList.ViewModels
{
	public class EditIsCompletedTaskViewModel
	{
		public int Id { get; set; }
		[Required]
		public bool IsCompleted { get; set; }
	}
}
