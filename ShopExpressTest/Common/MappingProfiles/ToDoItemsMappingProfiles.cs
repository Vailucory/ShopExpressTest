using AutoMapper;
using ShopExpressTest.Models;
using ShopExpressTest.ViewModels;

namespace ShopExpressTest.Common.MappingProfiles;

public class ToDoItemsMappingProfiles : Profile
{
    public ToDoItemsMappingProfiles()
    {
        CreateMap<ToDoItem, ToDoItemReadViewModel>();
        CreateMap<ToDoItem, ToDoItemEditViewModel>();
        CreateMap<ToDoItem, ToDoItemDeleteViewModel>();

        CreateMap<ToDoItemReadViewModel, ToDoItemEditViewModel>();  
    }
}
