namespace GShop.ProductApi.Models;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock { get; set; }
    public string? ImageURL { get; set; }
    //Relacionamento 1 para 1 -> 1 produto só tem 1 categoria
    public Category? Category{ get; set;}
    public Guid CategoryId { get; set; }
}
