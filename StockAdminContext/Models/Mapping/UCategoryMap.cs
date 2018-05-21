using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UCategoryMap : EntityTypeConfiguration<UCategory>
    {
        public UCategoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.Name });

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("UCategory");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
