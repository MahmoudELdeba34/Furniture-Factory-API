﻿

namespace FurnitureFactory.BAL
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<UpdateComponentDto> Components { get; set; } = new();
    }
}
