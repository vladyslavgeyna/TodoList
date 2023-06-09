﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using TodoList.DAL;
using TodoList.DAL.Repository;
using TodoList.DAL.Repository.Factory;
using TodoList.Enums;
using TodoList.Factory;
using TodoList.Service;
using TodoList.ViewModels;

namespace TodoList.Controllers
{
	public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public TaskController(IMapper mapper,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService,
            CookieStorageTypeService cookieStorageTypeService)
        {
            _taskRepository = RepositoryCookieBasedFactory.GetTaskRepository(cookieStorageTypeService, dapperContext, xmlStorageService);
            _categoryRepository = RepositoryCookieBasedFactory.GetCategoryRepository(cookieStorageTypeService, dapperContext, xmlStorageService);
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> EditIsComplete(EditIsCompletedTaskViewModel editIsCompletedTaskViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var task = await _taskRepository.GetByIdAsync(editIsCompletedTaskViewModel.Id);
            if (task is null)
            {
                return RedirectToAction("Index");
            }
            task.IsCompleted = editIsCompletedTaskViewModel.IsCompleted;
            await _taskRepository.UpdateByIdAsync(editIsCompletedTaskViewModel.Id, task);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var indexTaskViewModel = await IndexTaskViewModelFactory.CreateIndexTaskViewModel(_categoryRepository, _taskRepository);
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
                var indexTaskViewModel = await IndexTaskViewModelFactory.CreateIndexTaskViewModel(_categoryRepository, _taskRepository);
                indexTaskViewModel.CreateTaskViewModel = createTaskViewModel;
                return View("Index", indexTaskViewModel);
            }
            var task = _mapper.Map<Domain.Entity.Task>(createTaskViewModel);
            await _taskRepository.AddAsync(task);
            TempData["Message"] = new Dictionary<string, string>
            {
                { "Text", "The task was successfully added." },
                { "Class", "success" },
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTaskViewModel deleteTaskViewModel)
        {
            var task = await _taskRepository.GetByIdAsync(deleteTaskViewModel.Id);
            if (task is null)
            {
                return RedirectToAction("Index");
            }
            await _taskRepository.DeleteByIdAsync(deleteTaskViewModel.Id);
            return RedirectToAction("Index");
        }
    }
}
