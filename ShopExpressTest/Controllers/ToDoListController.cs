using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopExpressTest.DataAccess.Repositories;
using ShopExpressTest.DataAccess.UnitOfWork;
using ShopExpressTest.ViewModels;

namespace ShopExpressTest.Controllers;

public class ToDoListController : Controller
{
    private readonly ILogger<ToDoListController> _logger;

    private readonly IToDoItemRepository _repository;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public ToDoListController(
        ILogger<ToDoListController> logger,
        IToDoItemRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var itemsResult = _repository.GetAllToDoItems();

        if (!itemsResult.IsSucceeded)
        {
            return View(); 
        }

        return View(itemsResult.Value);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ToDoItemCreateViewModel createViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            RedirectToAction(nameof(Index));
        }

        _repository.Add(createViewModel);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int Id)
    {
        var result = _repository.Get(Id);

        if (result.IsSucceeded)
        {
            var readViewModel = result.Value;

            var editViewModel = _mapper.Map<ToDoItemEditViewModel>(readViewModel);
            return View(editViewModel);
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ToDoItemEditViewModel editViewModel, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            _repository.Update(editViewModel);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return RedirectToAction(nameof(Index));
        }
        return View(editViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCompletionStatus(ToDoItemEditViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false });
        }

        _repository.Update(model);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Json(new { success = true});
    }

    [HttpGet]
    public IActionResult Delete(int Id)
    {
        var result = _repository.Get(Id);

        if (result.IsSucceeded)
        {
            var readViewModel = result.Value;

            return View(readViewModel);
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int Id, CancellationToken cancellationToken)
    {
        var result = _repository.Get(Id);

        if (result.IsSucceeded)
        {
            var readViewModel = result.Value;

            var deleteViewModel = new ToDoItemDeleteViewModel(readViewModel.Id);

            _repository.Delete(deleteViewModel);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return RedirectToAction(nameof(Index));
    }
}
