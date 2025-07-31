using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.DAL;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}