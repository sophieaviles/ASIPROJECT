using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class DPI1_DownPaymentDetailMap : EntityTypeConfiguration<DPI1_DownPaymentDetail>
    {
        public DPI1_DownPaymentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDownPaymentDetail);

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

            // Table & Column Mappings
            this.ToTable("DPI1_DownPaymentDetail");
            this.Property(t => t.IdDownPaymentDetail).HasColumnName("IdDownPaymentDetail");
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
            this.Property(t => t.IdDownPaymentL).HasColumnName("IdDownPaymentL");
            this.Property(t => t.IdPaymentType).HasColumnName("IdPaymentType");
            this.Property(t => t.IdRoyalty).HasColumnName("IdRoyalty");

            // Relationships
            this.HasRequired(t => t.ODPI_DownPayment)
                .WithMany(t => t.DPI1_DownPaymentDetail)
                .HasForeignKey(d => d.IdDownPaymentL);
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.DPI1_DownPaymentDetail)
                .HasForeignKey(d => d.ItemCode);

        }
    }
}
