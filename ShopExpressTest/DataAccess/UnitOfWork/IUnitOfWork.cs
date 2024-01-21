using ShopExpressTest.Common;

namespace ShopExpressTest.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
}
