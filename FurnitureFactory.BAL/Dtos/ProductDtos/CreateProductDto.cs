

namespace FurnitureFactory.BAL { 
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<CreateComponentDto> Components { get; set; } = new();
    }
}
