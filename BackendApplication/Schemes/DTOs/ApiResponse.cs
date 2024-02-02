using System.Text.Json;

namespace Schemes.DTOs;

public class ApiResponse<T>
{
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
    public bool Success { get; set ; }
    public string Message { get; set; }
    public T? Content { get; set; }
    
    public ApiResponse(T content)
    {
        Success = true;
        Message = "Success";
        Content = content;
    }
    
    public ApiResponse(string message, bool success = false)
    {
        Success = success;
        Message = message;
    }
    
    public override string ToString() => JsonSerializer.Serialize(this);
}