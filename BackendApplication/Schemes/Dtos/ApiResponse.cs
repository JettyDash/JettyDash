using System.Text.Json;

namespace Schemes.Dtos;

public class ApiResponse<T>
{
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Success";
    public T? Content { get; set; }
    
    public ApiResponse(T content)
    {
        Content = content;
    }
    
    public ApiResponse(string message)
    {
        Success = false;
        Message = message;
    }
    
    public override string ToString() => JsonSerializer.Serialize(this);
}