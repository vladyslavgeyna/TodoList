using TodoList.Models;

namespace TodoList.ViewModels
{
    public class DetailsCategoryViewModel
    {
        public Category Category { get; set; } = null!;
        public IEnumerable<Models.Task> Tasks { get; set; } = new List<Models.Task>();
        public DeleteTaskViewModel DeleteTaskViewModel { get; set; } = null!;
        public DeleteCategoryViewModel DeleteCategoryViewModel { get; set; } = null!;
        public EditIsCompletedTaskViewModel EditIsCompletedTaskViewModel { get; set; } = null!;

    }
}
