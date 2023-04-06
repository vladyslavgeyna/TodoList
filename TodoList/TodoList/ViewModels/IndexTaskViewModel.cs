﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TodoList.Attributes;
using TodoList.Models;

namespace TodoList.ViewModels
{
	public class IndexTaskViewModel
	{
		public IEnumerable<Category> Categories { get; set; } = null!;
		public ICollection<SelectListItem> CategoriesSelect { get; set; } = new List<SelectListItem>();
		public CreateTaskViewModel CreateTaskViewModel { get; set; } = null!;
		public CreateCategoryViewModel CreateCategoryViewModel { get; set; } = null!;
    }
}
