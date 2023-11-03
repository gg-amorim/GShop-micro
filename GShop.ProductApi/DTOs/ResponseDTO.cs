namespace GShop.ProductApi.DTOs;

public class ResponseDTO
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "";
    public object? Data { get; set; }
}
