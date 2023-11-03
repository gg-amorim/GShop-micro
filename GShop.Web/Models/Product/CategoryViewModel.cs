using System.ComponentModel.DataAnnotations;

namespace GShop.Web.Models.Product;

public class CategoryViewModel
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

}
