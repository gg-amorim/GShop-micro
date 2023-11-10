namespace GShop.CartApi.Models;

public class CartHeader
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CouponCode { get; set; } = string.Empty;


}
