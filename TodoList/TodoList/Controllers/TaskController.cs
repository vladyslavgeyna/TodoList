﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Repository;
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

		private async Task<IndexTaskViewModel> _getIndexTaskViewModel()
		{
            var categories = await _categoryRepository.GetAllAsync();
            IndexTaskViewModel indexTaskViewModel = new IndexTaskViewModel
            {
                Categories = categories,
            };
            foreach (var category in categories)
            {
                indexTaskViewModel.CategoriesSelect.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name,
                });
            }
            return indexTaskViewModel;
        }
        public async Task<IActionResult> Index()
		{
            var indexTaskViewModel = await _getIndexTaskViewModel();
            return View(indexTaskViewModel);
		}
        public async Task<IActionResult> Create(CreateTaskViewModel createTaskViewModel)
        {
			if (!ModelState.IsValid)
			{
                var indexTaskViewModel = await _getIndexTaskViewModel();
                indexTaskViewModel.CreateTaskViewModel = createTaskViewModel;
                return View("Index", indexTaskViewModel);
			}
            var task = _mapper.Map<Models.Task>(createTaskViewModel);
            await _taskRepository.AddAsync(task);
			return RedirectToAction("Index");
        }
    }
}