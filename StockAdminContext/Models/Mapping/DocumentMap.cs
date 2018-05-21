using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.ObjType);

            // Properties
            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.DocumentName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Documents");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
        }
    }
}
