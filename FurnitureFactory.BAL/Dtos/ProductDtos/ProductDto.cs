﻿

namespace FurnitureFactory.BAL
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<ComponentDto> Components { get; set; } = new();
    }
}
