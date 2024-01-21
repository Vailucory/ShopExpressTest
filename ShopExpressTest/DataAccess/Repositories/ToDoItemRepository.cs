using AutoMapper;
using ShopExpressTest.Common;
using ShopExpressTest.DataAccess.UnitOfWork;
using ShopExpressTest.Models;
using ShopExpressTest.ViewModels;

namespace ShopExpressTest.DataAccess.Repositories;

/// <summary>
/// Repository to manipulate ToDoItems. 
/// After mutating data, you should call <see cref="IUnitOfWork"/> implementation to persist changes.
/// </summary>
public class ToDoItemRepository : IToDoItemRepository
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    public ToDoItemRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Result Add(ToDoItemCreateViewModel createViewModel)
    {
        if (createViewModel == null)
        {
            return Result.FromError(new ArgumentIsNullError(typeof(ToDoItem), nameof(createViewModel)));
        }

        ToDoItem item = new ToDoItem() { Name = createViewModel.Name};

        _context.ToDoItems.Add(item);

        return Result.Good();
    }

    public Result Delete(ToDoItemDeleteViewModel deleteViewModel)
    {
        int id = deleteViewModel.Id;

        ToDoItem? item = _context.ToDoItems.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return Result.FromError(new EntityNotFoundError(typeof(ToDoItem), id));
        }

        _context.ToDoItems.Remove(item);

        return Result.Good();
    }

    public Result<ToDoItemReadViewModel> Get(int id)
    {
        ToDoItem? item = _context.ToDoItems.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return Result<ToDoItemReadViewModel>
                .FromError(new EntityNotFoundError(typeof(ToDoItem), id));
        }

        var result = _mapper.Map<ToDoItemReadViewModel>(item);

        return Result<ToDoItemReadViewModel>.Good(result);
    }

    public Result<List<ToDoItemReadViewModel>> GetAllToDoItems()
    {
        var items = _context.ToDoItems.ToList();

        var result = _mapper.Map<List<ToDoItemReadViewModel>>(items);

        return Result<List<ToDoItemReadViewModel>>.Good(result);
    }

    public Result Update(ToDoItemEditViewModel editViewModel)
    {
        int id = editViewModel.Id;

        ToDoItem? item = _context.ToDoItems.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return Result<ToDoItemReadViewModel>
                .FromError(new EntityNotFoundError(typeof(ToDoItem), id));
        }

        item.Name = editViewModel.Name;

        item.IsCompleted = editViewModel.IsCompleted;

        _context.ToDoItems.Update(item);

        return Result.Good();
    }
}
