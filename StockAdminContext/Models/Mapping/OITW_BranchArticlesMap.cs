
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OITW_BranchArticlesMap : EntityTypeConfiguration<OITW_BranchArticles>
    {
        public OITW_BranchArticlesMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ItemCode, t.WhsCode });

            // Properties
            this.Property(t => t.ItemCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.WhsCode)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Locked)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OITW_BranchArticles");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.OnHand).HasColumnName("OnHand");
            this.Property(t => t.OnHand1).HasColumnName("OnHand1");
            this.Property(t => t.IsCommited).HasColumnName("IsCommited");
            this.Property(t => t.OnOrder).HasColumnName("OnOrder");
            this.Property(t => t.MinStock).HasColumnName("MinStock");
            this.Property(t => t.MaxStock).HasColumnName("MaxStock");
            this.Property(t => t.MinOrder).HasColumnName("MinOrder");
            this.Property(t => t.Locked).HasColumnName("Locked");
            this.Property(t => t.LastSyncDateL).HasColumnName("LastSyncDateL");

            // Relationships
            this.HasRequired(t => t.OITM_Articles)
                .WithMany(t => t.OITW_BranchArticles)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.OWHS_Branch)
                .WithMany(t => t.OITW_BranchArticles)
                .HasForeignKey(d => d.WhsCode);

        }
    }
}
