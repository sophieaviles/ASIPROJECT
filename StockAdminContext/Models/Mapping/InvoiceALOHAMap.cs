using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class InvoiceALOHAMap : EntityTypeConfiguration<InvoiceALOHA>
    {
        public InvoiceALOHAMap()
        {
            // Primary Key
            this.HasKey(t => t.IdInvoiceALOHA);

            // Properties
            this.Property(t => t.State)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.correlative)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("InvoiceALOHA");
            this.Property(t => t.IdInvoiceALOHA).HasColumnName("IdInvoiceALOHA");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Credit).HasColumnName("Credit");
            this.Property(t => t.Cash).HasColumnName("Cash");
            this.Property(t => t.correlative).HasColumnName("correlative");
        }
    }
}
