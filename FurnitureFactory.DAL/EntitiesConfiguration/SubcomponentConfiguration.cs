using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FurnitureFactory.DAL.EntitiesConfiguration
{
    public class SubcomponentConfiguration : IEntityTypeConfiguration<Subcomponent>
    {
        public void Configure(EntityTypeBuilder<Subcomponent> builder)
        {
            builder.ToTable("Subcomponents");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.Material)
                   .HasMaxLength(100);

            builder.Property(s => s.CustomNotes)
                   .HasMaxLength(500);

            builder.Property(s => s.Count)
                   .IsRequired();

            // Owned: DetailSize
            builder.OwnsOne(s => s.DetailSize, ds =>
            {
                ds.Property(p => p.Length).HasColumnName("DetailLength").HasPrecision(10, 2);
                ds.Property(p => p.Width).HasColumnName("DetailWidth").HasPrecision(10, 2);
                ds.Property(p => p.Thickness).HasColumnName("DetailThickness").HasPrecision(10, 2);
            });

            // Owned: CuttingSize
            builder.OwnsOne(s => s.CuttingSize, cs =>
            {
                cs.Property(p => p.Length).HasColumnName("CuttingLength").HasPrecision(10, 2);
                cs.Property(p => p.Width).HasColumnName("CuttingWidth").HasPrecision(10, 2);
                cs.Property(p => p.Thickness).HasColumnName("CuttingThickness").HasPrecision(10, 2);
            });

            // Owned: FinalSize
            builder.OwnsOne(s => s.FinalSize, fs =>
            {
                fs.Property(p => p.Length).HasColumnName("FinalLength").HasPrecision(10, 2);
                fs.Property(p => p.Width).HasColumnName("FinalWidth").HasPrecision(10, 2);
                fs.Property(p => p.Thickness).HasColumnName("FinalThickness").HasPrecision(10, 2);
            });

            // Owned: VeneerLayer
            builder.OwnsOne(s => s.VeneerLayer, vl =>
            {
                vl.Property(p => p.Inner).HasColumnName("VeneerInner").HasMaxLength(100);
                vl.Property(p => p.Outer).HasColumnName("VeneerOuter").HasMaxLength(100);
            });
        }

    }
}
