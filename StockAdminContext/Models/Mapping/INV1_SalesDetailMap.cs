using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class INV1_SalesDetailMap : EntityTypeConfiguration<INV1_SalesDetail>
    {
        public INV1_SalesDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdSaleDetail);

            // Properties
            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.Dscription)
                .HasMaxLength(100);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            this.Property(t => t.AcctCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.OcrCode)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.TaxStatus)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("INV1_SalesDetail");
            this.Property(t => t.IdSaleDetail).HasColumnName("IdSaleDetail");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.LineNum).HasColumnName("LineNum");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Dscription).HasColumnName("Dscription");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.AcctCode).HasColumnName("AcctCode");
            this.Property(t => t.OcrCode).HasColumnName("OcrCode");
            this.Property(t => t.IdSaleL).HasColumnName("IdSaleL");
            this.Property(t => t.IdPaymentType).HasColumnName("IdPaymentType");
            this.Property(t => t.IdRoyalty).HasColumnName("IdRoyalty");
            this.Property(t => t.TaxStatus).HasColumnName("TaxStatus");

            // Relationships
            this.HasRequired(t => t.OINV_Sales)
                .WithMany(t => t.INV1_SalesDetail)
                .HasForeignKey(d => d.IdSaleL);
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.INV1_SalesDetail)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.PaymentType)
                .WithMany(t => t.INV1_SalesDetail)
                .HasForeignKey(d => d.IdPaymentType);
            this.HasRequired(t => t.PaymentType1)
                .WithMany(t => t.INV1_SalesDetail1)
                .HasForeignKey(d => d.IdRoyalty);

        }
    }
}
