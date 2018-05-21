using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class IGN1_GoodsReceiptDetailMap : EntityTypeConfiguration<IGN1_GoodsReceiptDetail>
    {
        public IGN1_GoodsReceiptDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdGoodReceiptDetail);

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

            this.Property(t => t.OITM_Articles_ItemCode)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("IGN1_GoodsReceiptDetail");
            this.Property(t => t.IdGoodReceiptDetail).HasColumnName("IdGoodReceiptDetail");
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
            this.Property(t => t.IdGoodReceiptL).HasColumnName("IdGoodReceiptL");
            this.Property(t => t.OITM_Articles_ItemCode).HasColumnName("OITM_Articles_ItemCode");

            // Relationships
            this.HasRequired(t => t.OIGN_GoodsReceipt)
                .WithMany(t => t.IGN1_GoodsReceiptDetail)
                .HasForeignKey(d => d.IdGoodReceiptL);
            this.HasRequired(t => t.OITM_Articles)
                .WithMany(t => t.IGN1_GoodsReceiptDetail)
                .HasForeignKey(d => d.OITM_Articles_ItemCode);

        }
    }
}
