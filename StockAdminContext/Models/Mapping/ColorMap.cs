using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class ColorMap : EntityTypeConfiguration<Color>
    {
        public ColorMap()
        {
            // Primary Key
            this.HasKey(t => t.IdColors);

            // Properties
            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Colors");
            this.Property(t => t.IdColors).HasColumnName("IdColors");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CoverIdCover).HasColumnName("CoverIdCover");
        }
    }
}
