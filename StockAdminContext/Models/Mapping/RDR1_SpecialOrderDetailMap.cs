using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class RDR1_SpecialOrderDetailMap : EntityTypeConfiguration<RDR1_SpecialOrderDetail>
    {
        public RDR1_SpecialOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdSpecialOrderDetail);

            // Properties
            this.Property(t => t.AcctCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Dscription)
                .HasMaxLength(100);

            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.OcrCode)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("RDR1_SpecialOrderDetail");
            this.Property(t => t.IdSpecialOrderDetail).HasColumnName("IdSpecialOrderDetail");
            this.Property(t => t.AcctCode).HasColumnName("AcctCode");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.Dscription).HasColumnName("Dscription");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.LineNum).HasColumnName("LineNum");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.OcrCode).HasColumnName("OcrCode");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.IdSpecialOrder).HasColumnName("IdSpecialOrder");

            // Relationships
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.RDR1_SpecialOrderDetail)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.ORDR_SpecialOrder)
                .WithMany(t => t.RDR1_SpecialOrderDetail)
                .HasForeignKey(d => d.IdSpecialOrder);

        }
    }
}
