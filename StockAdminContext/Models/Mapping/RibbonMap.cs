using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class RibbonMap : EntityTypeConfiguration<Ribbon>
    {
        public RibbonMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRibbon);

            // Properties
            this.Property(t => t.U_SUBLINEA)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Ribbon");
            this.Property(t => t.IdRibbon).HasColumnName("IdRibbon");
            this.Property(t => t.ColorsIdColors).HasColumnName("ColorsIdColors");
            this.Property(t => t.U_SUBLINEA).HasColumnName("U_SUBLINEA");

            // Relationships
            this.HasRequired(t => t.Color)
                .WithMany(t => t.Ribbons)
                .HasForeignKey(d => d.ColorsIdColors);

        }
    }
}
