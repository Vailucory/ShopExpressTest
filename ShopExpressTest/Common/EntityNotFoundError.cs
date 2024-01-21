namespace ShopExpressTest.Common;

public class EntityNotFoundError : Error
{
    public EntityNotFoundError(Type entity, string keyValue, string propertyName = "Id")
        : base(propertyName, $"{entity.Name} with {propertyName} equals {keyValue} not found.")
    {
    }

    public EntityNotFoundError(Type entity, int keyValue, string propertyName = "Id")
        : this(entity, keyValue.ToString(), propertyName)
    {
    }

    public EntityNotFoundError(Type entity, Guid keyValue, string propertyName = "Id")
        : this(entity, keyValue.ToString(), propertyName)
    {
    }
}
