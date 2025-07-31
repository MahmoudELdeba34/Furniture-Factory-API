

namespace FurnitureFactory.BAL
{
    public class ComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public List<SubcomponentDto> Subcomponents { get; set; } = new();
    }
}
