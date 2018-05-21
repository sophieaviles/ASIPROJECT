using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class DetailInvoiceALOHAMap : EntityTypeConfiguration<DetailInvoiceALOHA>
    {
        public DetailInvoiceALOHAMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDetailInvoiceALOHA);

            // Properties
            this.Property(t => t.EntryId)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.Property1)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DetailInvoiceALOHA");
            this.Property(t => t.IdDetailInvoiceALOHA).HasColumnName("IdDetailInvoiceALOHA");
            this.Property(t => t.InvoiceALOHAIdInvoiceALOHA).HasColumnName("InvoiceALOHAIdInvoiceALOHA");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.EntryId).HasColumnName("EntryId");
            this.Property(t => t.Property1).HasColumnName("Property1");
            this.Property(t => t.Hour).HasColumnName("Hour");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Sysdate).HasColumnName("Sysdate");
            this.Property(t => t.TermId).HasColumnName("TermId");
            this.Property(t => t.Type).HasColumnName("Type");

            // Relationships
            this.HasRequired(t => t.InvoiceALOHA)
                .WithMany(t => t.DetailInvoiceALOHAs)
                .HasForeignKey(d => d.InvoiceALOHAIdInvoiceALOHA);

        }
    }
}
