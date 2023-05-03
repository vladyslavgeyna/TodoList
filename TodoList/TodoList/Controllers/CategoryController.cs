using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Factory;
using TodoList.ViewModels;
using TodoList.DAL.Repository;
using TodoList.Service;
using TodoList.DAL;
using TodoList.Domain.Entity;
using TodoList.DAL.Repository.Factory;

namespace TodoList.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper,
            XmlStorageService xmlStorageService,
            DapperContext dapperContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _taskRepository = RepositoryCookieBasedFactory.GetTaskRepository(httpContextAccessor?.HttpContext, dapperContext, xmlStorageService);
            _categoryRepository = RepositoryCookieBasedFactory.GetCategoryRepository(httpContextAccessor?.HttpContext, dapperContext, xmlStorageService);
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
                var indexTaskViewModel = await IndexTaskViewModelFactory.CreateIndexTaskViewModel(_categoryRepository, _taskRepository);
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
            var tasks = (await _taskRepository.GetAllAsync()).Where(task => task.CategoryId == category.Id);
            foreach (var task in tasks)
            {
                task.CategoryId = -1;
                await _taskRepository.UpdateByIdAsync(task.Id, task);
            }
            return RedirectToAction("Index", "Task");
        }
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return RedirectToAction("Index", "Task");
            }
            var tasks = (await _taskRepository.GetAllAsync()).Where(task => task.CategoryId == id).ToList();
            var detailsCategoryViewModel = new DetailsCategoryViewModel
            {
                Category = category,
                Tasks = tasks
            };
            return View(detailsCategoryViewModel);
        }
    }
}
