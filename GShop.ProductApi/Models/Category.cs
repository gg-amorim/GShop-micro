namespace GShop.ProductApi.Models;

public class Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    //Referencia 1 para N -> 1 categoria para N produtos
    public ICollection<Product>? Products { get; set; }
}
