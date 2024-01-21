namespace ShopExpressTest.Common;

public class Error
{
    public Error(string propertyName, string message)
    {
        PropertyName = propertyName;
        Message = message;
    }

    public static Error FromException(Exception exception)
    {
        string unknownCaller = "Unknown caller";

        string defaultMessage = "Unknown exception occurs";

        if (exception.InnerException is not null)
        {
            //exception rethrown
            return new Error(exception.InnerException.TargetSite?.Name ?? unknownCaller,
                exception?.Message ?? defaultMessage);
        }
        return new Error(exception?.TargetSite?.Name ?? unknownCaller,
            exception?.Message ?? defaultMessage);
    }

    public string Message { get; set; }

    public string PropertyName { get; set; }
}

