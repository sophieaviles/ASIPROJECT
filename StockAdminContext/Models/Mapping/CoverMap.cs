using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class CoverMap : EntityTypeConfiguration<Cover>
    {
        public CoverMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCover);

            // Properties
            this.Property(t => t.U_SUBLINEA)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Cover");
            this.Property(t => t.IdCover).HasColumnName("IdCover");
            this.Property(t => t.U_SUBLINEA).HasColumnName("U_SUBLINEA");
            this.Property(t => t.IdColors).HasColumnName("IdColors");

            // Relationships
            this.HasRequired(t => t.Color)
                .WithMany(t => t.Covers)
                .HasForeignKey(d => d.IdColors);

        }
    }
}
