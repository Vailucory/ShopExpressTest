using ShopExpressTest.Common;

namespace ShopExpressTest.DataAccess.UnitOfWork;

/// <summary>
/// Persists changes to database
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Good();
        }
        catch (Exception e)
        {
            return Result.FromError(Error.FromException(e));
        }
    }
}