using GShop.ProductApi.Models;
using System.ComponentModel.DataAnnotations;

namespace GShop.ProductApi.DTOs;

public class CategoryDTO
{
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(4)]
    [MaxLength(100)]
    public string? Name { get; set; }
    public ICollection<ProductDTO>? Products { get; set; }
}
