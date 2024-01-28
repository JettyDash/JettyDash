using System.Text.Json;

namespace Schemes.Dtos;

public class ApiResponse<T>(T content)
{
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Success";
    public T Content { get; set; } = content;

    public override string ToString() => JsonSerializer.Serialize(this);
}