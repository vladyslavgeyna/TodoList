using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Repository;
using TodoList.Utils;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
	public class TaskController : Controller
	{
		private readonly ITaskRepository _taskRepository;
		private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository taskRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper)
		{
			_taskRepository = taskRepository;
			_categoryRepository = categoryRepository;
            _mapper = mapper;
		}

        public async Task<IActionResult> Index()
		{
            var indexTaskViewModel = await IndexTaskViewModelCreator.CreateIndexTaskViewModel(_categoryRepository, _taskRepository);
            return View(indexTaskViewModel);
		}
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel createTaskViewModel)
        {
			if (!ModelState.IsValid)
			{
                TempData["Message"] = new Dictionary<string, string>
                {
                    { "Text", "Check out entered data." },
                    { "Class", "danger" },
                };
                var indexTaskViewModel = await IndexTaskViewModelCreator.CreateIndexTaskViewModel(_categoryRepository, _taskRepository);
                indexTaskViewModel.CreateTaskViewModel = createTaskViewModel;
                return View("Index", indexTaskViewModel);
			}
            var task = _mapper.Map<Models.Task>(createTaskViewModel);
            await _taskRepository.AddAsync(task);
            TempData["Message"] = new Dictionary<string, string>
            {
                { "Text", "The task was successfully added." },
                { "Class", "success" },
            };
			return RedirectToAction("Index");
        }
    }
}
