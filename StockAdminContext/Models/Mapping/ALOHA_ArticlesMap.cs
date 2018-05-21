using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class ALOHA_ArticlesMap : EntityTypeConfiguration<ALOHA_Articles>
    {
        public ALOHA_ArticlesMap()
        {
            // Primary Key
            this.HasKey(t => t.IdALOHA_Articles);

            // Properties
            this.Property(t => t.Categories)
                .IsRequired();

            this.Property(t => t.code)
                .IsRequired();

            this.Property(t => t.State)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Price)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ALOHA_Articles");
            this.Property(t => t.IdALOHA_Articles).HasColumnName("IdALOHA_Articles");
            this.Property(t => t.Categories).HasColumnName("Categories");
            this.Property(t => t.code).HasColumnName("code");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DetailInvoiceALOHAIdDetailInvoiceALOHA).HasColumnName("DetailInvoiceALOHAIdDetailInvoiceALOHA");

            // Relationships
           

        }
    }
}
