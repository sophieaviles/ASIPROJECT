using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class WTR1_TransferDetailsMap : EntityTypeConfiguration<WTR1_TransferDetails>
    {
        public WTR1_TransferDetailsMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTransferDetailL);

            // Properties
            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.Dscription)
                .HasMaxLength(100);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            this.Property(t => t.LineStatus)
                .HasMaxLength(1);

            this.Property(t => t.UseBaseUn)
                .HasMaxLength(1);

            this.Property(t => t.UnitMsr)
                .HasMaxLength(20);

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.IdTransferDetailL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("WTR1_TransferDetails");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.LineNum).HasColumnName("LineNum");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Dscription).HasColumnName("Dscription");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.OpenQty).HasColumnName("OpenQty");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.LineStatus).HasColumnName("LineStatus");
            this.Property(t => t.UseBaseUn).HasColumnName("UseBaseUn");
            this.Property(t => t.UnitMsr).HasColumnName("UnitMsr");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.IdTransferL).HasColumnName("IdTransferL");
            this.Property(t => t.IdTransferDetailL).HasColumnName("IdTransferDetailL");

            // Relationships
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.WTR1_TransferDetails)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.OWTR_Transfers)
                .WithMany(t => t.WTR1_TransferDetails)
                .HasForeignKey(d => d.IdTransferL);

        }
    }
}
