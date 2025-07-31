using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.DAL;

public class Component
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } // Number of times this component is used in product

    // Foreign Key
    public int ProductId { get; set; }

    // Navigation
    public Product Product { get; set; } = null!;

    public ICollection<Subcomponent> Subcomponents { get; set; } = new List<Subcomponent>();
}