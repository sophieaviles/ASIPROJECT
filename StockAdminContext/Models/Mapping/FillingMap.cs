using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class FillingMap : EntityTypeConfiguration<Filling>
    {
        public FillingMap()
        {
            // Primary Key
            this.HasKey(t => t.IdFilling);

            // Properties
            this.Property(t => t.U_SUBLINEA)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Filling");
            this.Property(t => t.IdFilling).HasColumnName("IdFilling");
            this.Property(t => t.U_SUBLINEA).HasColumnName("U_SUBLINEA");
            this.Property(t => t.ColorsIdColors).HasColumnName("ColorsIdColors");

            // Relationships
            this.HasRequired(t => t.Color)
                .WithMany(t => t.Fillings)
                .HasForeignKey(d => d.ColorsIdColors);

        }
    }
}
