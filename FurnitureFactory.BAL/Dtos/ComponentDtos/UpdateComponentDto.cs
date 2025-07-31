

namespace FurnitureFactory.BAL
{
    public class UpdateComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public List<UpdateSubcomponentDto> Subcomponents { get; set; } = new();
    }
}
