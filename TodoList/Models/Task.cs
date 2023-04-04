using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
	public class Task
	{
		[Key]
		public int Id { get; set; }
		public string Description { get; set; } = null!;
		public DateTime DueDate { get; set; }
		public DateTime DateOfCreation { get; set; }
		public bool IsCompleted { get; set; }
		public int? CategoryId { get; set; }
		public Category? Category { get; set; }
	}
}
