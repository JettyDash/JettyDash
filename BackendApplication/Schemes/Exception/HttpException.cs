namespace Schemes.Exception;

public class HttpException : System.Exception
{
    public int StatusCode { get; }

    public HttpException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}