using ShopExpressTest.Common;
using ShopExpressTest.ViewModels;

namespace ShopExpressTest.DataAccess.Repositories;

public interface IToDoItemRepository
{
    Result Add(ToDoItemCreateViewModel item);
    Result Delete(ToDoItemDeleteViewModel deleteViewModel);
    Result<ToDoItemReadViewModel> Get(int id);
    Result Update(ToDoItemEditViewModel editViewModel);
    Result<List<ToDoItemReadViewModel>> GetAllToDoItems();
}
