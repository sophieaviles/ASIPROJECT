using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class RPC1_SupplierCreditNoteDetailMap : EntityTypeConfiguration<RPC1_SupplierCreditNoteDetail>
    {
        public RPC1_SupplierCreditNoteDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdSupplierCreditNoteDetail);

            // Properties
            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            this.Property(t => t.AcctCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.OcrCode)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("RPC1_SupplierCreditNoteDetail");
            this.Property(t => t.IdSupplierCreditNoteDetail).HasColumnName("IdSupplierCreditNoteDetail");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.LineNum).HasColumnName("LineNum");
            this.Property(t => t.BaseDocNum).HasColumnName("BaseDocNum");
            this.Property(t => t.BaseEntry).HasColumnName("BaseEntry");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.LineTotal).HasColumnName("LineTotal");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.AcctCode).HasColumnName("AcctCode");
            this.Property(t => t.OcrCode).HasColumnName("OcrCode");
            this.Property(t => t.IdSupplierCreditNote).HasColumnName("IdSupplierCreditNote");

            // Relationships
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.RPC1_SupplierCreditNoteDetail)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.ORPC_SupplierCreditNotes)
                .WithMany(t => t.RPC1_SupplierCreditNoteDetail)
                .HasForeignKey(d => d.IdSupplierCreditNote);

        }
    }
}
