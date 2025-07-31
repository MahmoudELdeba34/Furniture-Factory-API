
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FurnitureFactory.DAL
{

    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.ToTable("Components");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            // Relationship: One Component → Many Subcomponents
            builder.HasMany(c => c.Subcomponents)
                   .WithOne(s => s.Component)
                   .HasForeignKey(s => s.ComponentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
