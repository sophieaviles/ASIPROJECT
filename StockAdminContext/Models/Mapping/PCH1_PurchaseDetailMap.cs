using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class PCH1_PurchaseDetailMap : EntityTypeConfiguration<PCH1_PurchaseDetail>
    {
        public PCH1_PurchaseDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPurchaseDetail);

            // Properties
            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            this.Property(t => t.AcctCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.UseBaseUn)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.TaxCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            this.Property(t => t.StateL)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("PCH1_PurchaseDetail");
            this.Property(t => t.IdPurchaseDetail).HasColumnName("IdPurchaseDetail");
            this.Property(t => t.IdPurchase).HasColumnName("IdPurchase");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.LineNum).HasColumnName("LineNum");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.AcctCode).HasColumnName("AcctCode");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.UseBaseUn).HasColumnName("UseBaseUn");
            this.Property(t => t.TaxCode).HasColumnName("TaxCode");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.StateL).HasColumnName("StateL");

            // Relationships
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.PCH1_PurchaseDetail)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.OPCH_Purchase)
                .WithMany(t => t.PCH1_PurchaseDetail)
                .HasForeignKey(d => d.IdPurchase);

        }
    }
}
