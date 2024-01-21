namespace ShopExpressTest.Common;

public class ArgumentIsNullError : Error
{
    public ArgumentIsNullError(Type entity, string propertyName) 
        : base(propertyName, $"{propertyName} of type {entity.Name} is null.")
    {
    }
}
