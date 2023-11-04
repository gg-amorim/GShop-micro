using System.ComponentModel.DataAnnotations;

namespace GShop.Web.Models.Product;

public class ProductViewModel
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public long Stock { get; set; }
    [Required]
    public string ImageURL { get; set; }
    public string CategoryName { get; set; }
    [Display(Name ="Categories")]
    public Guid CategoryId { get; set; }
}
