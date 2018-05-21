using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class IGE1_GoodsIssueDetailMap : EntityTypeConfiguration<IGE1_GoodsIssueDetail>
    {
        public IGE1_GoodsIssueDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdGoodIssueDetail);

            // Properties
            this.Property(t => t.LineStatus)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.Dscription)
                .HasMaxLength(100);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            this.Property(t => t.AcctCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.UseBaseUn)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.UnitMsr)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CodeBars)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            this.Property(t => t.CreatedByL)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("IGE1_GoodsIssueDetail");
            this.Property(t => t.IdGoodIssueDetail).HasColumnName("IdGoodIssueDetail");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.LineNum).HasColumnName("LineNum");
            this.Property(t => t.LineStatus).HasColumnName("LineStatus");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Dscription).HasColumnName("Dscription");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.AcctCode).HasColumnName("AcctCode");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.UseBaseUn).HasColumnName("UseBaseUn");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.UnitMsr).HasColumnName("UnitMsr");
            this.Property(t => t.CodeBars).HasColumnName("CodeBars");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.IdGoodIssueL).HasColumnName("IdGoodIssueL");

            // Relationships
            this.HasRequired(t => t.OIGE_GoodsIssues)
                .WithMany(t => t.IGE1_GoodsIssueDetail)
                .HasForeignKey(d => d.IdGoodIssueL);
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.IGE1_GoodsIssueDetail)
                .HasForeignKey(d => d.ItemCode);

        }
    }
}
