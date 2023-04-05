using AutoMapper;
using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTaskViewModel, Models.Task>();
            CreateMap<Models.Task, CreateTaskViewModel>();
            CreateMap<CreateCategoryViewModel, Category>();
            CreateMap<Category, CreateCategoryViewModel>();
        }
    }
}
