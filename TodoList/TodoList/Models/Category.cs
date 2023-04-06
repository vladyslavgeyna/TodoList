using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public List<Task> Tasks { get; set; } = new ();
	}
}
