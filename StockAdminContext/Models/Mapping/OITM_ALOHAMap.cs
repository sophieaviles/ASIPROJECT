using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OITM_ALOHAMap : EntityTypeConfiguration<OITM_ALOHA>
    {
        public OITM_ALOHAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdALOHA_Articles, t.ItemCode });

            // Properties
            this.Property(t => t.IdALOHA_Articles)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ItemCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OITM_Articles_ItemCode)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("OITM_ALOHA");
            this.Property(t => t.IdALOHA_Articles).HasColumnName("IdALOHA_Articles");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.OITM_Articles_ItemCode).HasColumnName("OITM_Articles_ItemCode");

            // Relationships
            this.HasRequired(t => t.ALOHA_Articles)
                .WithMany(t => t.OITM_ALOHA)
                .HasForeignKey(d => d.IdALOHA_Articles);
            this.HasRequired(t => t.OITM_Articles)
                .WithMany(t => t.OITM_ALOHA)
                .HasForeignKey(d => d.OITM_Articles_ItemCode);

        }
    }
}
