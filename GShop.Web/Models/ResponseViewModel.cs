namespace GShop.Web.Models;

public class ResponseViewModel
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "";
    public object? Data { get; set; }
}
