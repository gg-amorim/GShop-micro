using GShop.ProductApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GShop.ProductApi.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(4)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Price is Required")]
    [Range(0, double.MaxValue, ErrorMessage = "The price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(4)]
    [MaxLength(255)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Stock is Required")]
    [Range(0, 9999)]
    public long Stock { get; set; }
    public string? ImageURL { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
    public Guid CategoryId { get; set; }
}
