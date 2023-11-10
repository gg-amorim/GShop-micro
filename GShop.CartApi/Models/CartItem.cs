namespace GShop.CartApi.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; } = 1;
    public Guid ProductId { get; set; }
    public Guid CartHeaderId { get; set; }
    public Product Product { get; set; } = new Product();
    public CartHeader CartHeader { get; set; } = new CartHeader();

}
