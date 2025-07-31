using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.DAL;

public class Subcomponent
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Material { get; set; } = string.Empty;
    public string CustomNotes { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Count { get; set; }

    // Calculated: TotalQuantity = Count * Component.Quantity
    public int TotalQuantity => Count * Component?.Quantity ?? 0;

    public DetailSize DetailSize { get; set; } = new();
    public CuttingSize CuttingSize { get; set; } = new();
    public FinalSize FinalSize { get; set; } = new();

    public VeneerLayer VeneerLayer { get; set; } = new();

    // Foreign Key
    public int ComponentId { get; set; }

    // Navigation
    public Component Component { get; set; } = null!;
}

