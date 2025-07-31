

namespace FurnitureFactory.BAL
{
    public class CreateSubcomponentDto
    {
        public string Name { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string CustomNotes { get; set; } = string.Empty;
        public int Count { get; set; }
        public int ComponentId { get; set; }  


        public DetailSizeDto DetailSize { get; set; } = new();
        public CuttingSizeDto CuttingSize { get; set; } = new();
        public FinalSizeDto FinalSize { get; set; } = new();
        public VeneerLayerDto VeneerLayer { get; set; } = new();
    }
}
