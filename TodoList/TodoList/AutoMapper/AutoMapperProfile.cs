using AutoMapper;
using TodoList.Domain.Entity;
using TodoList.ViewModels;

namespace TodoList.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTaskViewModel, Domain.Entity.Task>();
            CreateMap<Domain.Entity.Task, CreateTaskViewModel>();
            CreateMap<CreateCategoryViewModel, Category>();
            CreateMap<Category, CreateCategoryViewModel>();
        }
    }
}
