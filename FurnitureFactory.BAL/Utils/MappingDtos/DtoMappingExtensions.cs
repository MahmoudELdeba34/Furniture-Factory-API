using FurnitureFactory.DAL;


namespace FurnitureFactory.BAL.Utils.MappingDtos
{
    public static class DtoMappingExtensions
    {
        //  Product → ProductDto
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Components = product.Components.Select(c => c.ToDto()).ToList()
            };
        }

        //  Component → ComponentDto
        public static ComponentDto ToDto(this Component component)
        {
            return new ComponentDto
            {
                Id = component.Id,
                Name = component.Name,
                Quantity = component.Quantity,
                Subcomponents = component.Subcomponents.Select(s => s.ToDto(component.Quantity)).ToList()
            };
        }

        //  Subcomponent → SubcomponentDto (with component quantity for TotalQuantity)
        public static SubcomponentDto ToDto(this Subcomponent subcomponent, int componentQuantity = 1)
        {
            return new SubcomponentDto
            {
                Id = subcomponent.Id,
                Name = subcomponent.Name,
                Material = subcomponent.Material,
                CustomNotes = subcomponent.CustomNotes,
                Count = subcomponent.Count,
                TotalQuantity = subcomponent.Count * componentQuantity,

                DetailSize = new DetailSizeDto
                {
                    Length = subcomponent.DetailSize.Length,
                    Width = subcomponent.DetailSize.Width,
                    Thickness = subcomponent.DetailSize.Thickness
                },
                CuttingSize = new CuttingSizeDto
                {
                    Length = subcomponent.CuttingSize.Length,
                    Width = subcomponent.CuttingSize.Width,
                    Thickness = subcomponent.CuttingSize.Thickness
                },
                FinalSize = new FinalSizeDto
                {
                    Length = subcomponent.FinalSize.Length,
                    Width = subcomponent.FinalSize.Width,
                    Thickness = subcomponent.FinalSize.Thickness
                },
                VeneerLayer = new VeneerLayerDto 
                {
                    Inner = subcomponent.VeneerLayer.Inner,
                    Outer = subcomponent.VeneerLayer.Outer
                }
            };
        }

        public static DetailSize ToEntity(this DetailSizeDto dto) => new()
        {
            Length = dto.Length,
            Width = dto.Width,
            Thickness = dto.Thickness
        };

        public static CuttingSize ToEntity(this CuttingSizeDto dto) => new()
        {
            Length = dto.Length,
            Width = dto.Width,
            Thickness = dto.Thickness
        };

        public static FinalSize ToEntity(this FinalSizeDto dto) => new()
        {
            Length = dto.Length,
            Width = dto.Width,
            Thickness = dto.Thickness
        };

        public static VeneerLayer ToEntity(this VeneerLayerDto dto) => new()
        {
            Inner = dto.Inner,
            Outer = dto.Outer
        };

    }
}
