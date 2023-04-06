using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Repository;
using TodoList.Utils;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository,
            IMapper mapper,
            ITaskRepository taskRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = new Dictionary<string, string>
                {
                    { "Text", "Check out entered data." },
                    { "Class", "danger" },
                };
                var indexTaskViewModel = await IndexTaskViewModelCreator.CreateIndexTaskViewModel(_categoryRepository, _taskRepository);
                indexTaskViewModel.CreateCategoryViewModel = createCategoryViewModel;
                return View("~/Views/Task/Index.cshtml", indexTaskViewModel);
            }
            var category = _mapper.Map<Category>(createCategoryViewModel);
            await _categoryRepository.AddAsync(category);
            TempData["Message"] = new Dictionary<string, string>
            {
                { "Text", "The category was successfully added." },
                { "Class", "success" },
            };
            return RedirectToAction("Index", "Task");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCategoryViewModel deleteCategoryViewModel)
        {
            var category = await _categoryRepository.GetByIdAsync(deleteCategoryViewModel.Id);
            if (category is null)
            {
                return RedirectToAction("Index", "Task");
            }
            await _categoryRepository.DeleteByIdAsync(deleteCategoryViewModel.Id);
            return RedirectToAction("Index", "Task");
        }
    }
}
