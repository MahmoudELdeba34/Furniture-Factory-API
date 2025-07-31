

namespace FurnitureFactory.BAL
{
    public class CreateComponentDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public List<CreateSubcomponentDto> Subcomponents { get; set; } = new();
    }
}
