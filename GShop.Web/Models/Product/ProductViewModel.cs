namespace GShop.Web.Models.Product;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock { get; set; }
    public string? ImageURL { get; set; }
    public string? CategoryName { get; set; }
    public Guid CategoryId { get; set; }
}
