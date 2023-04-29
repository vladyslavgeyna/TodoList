using TodoList.Domain.Entity;

namespace TodoList.ViewModels
{
    public class DetailsCategoryViewModel
    {
        public Category Category { get; set; } = null!;
        public IEnumerable<Domain.Entity.Task> Tasks { get; set; } = new List<Domain.Entity.Task>();
        public DeleteTaskViewModel DeleteTaskViewModel { get; set; } = null!;
        public DeleteCategoryViewModel DeleteCategoryViewModel { get; set; } = null!;
        public EditIsCompletedTaskViewModel EditIsCompletedTaskViewModel { get; set; } = null!;

    }
}
