using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class RIN1_ClientCreditNoteDetailMap : EntityTypeConfiguration<RIN1_ClientCreditNoteDetail>
    {
        public RIN1_ClientCreditNoteDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IdClientCreditNoteDetailL);

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
            this.ToTable("RIN1_ClientCreditNoteDetail");
            this.Property(t => t.IdClientCreditNoteDetailL).HasColumnName("IdClientCreditNoteDetailL");
            this.Property(t => t.IdClientCreditNoteL).HasColumnName("IdClientCreditNoteL");
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

            // Relationships
            this.HasOptional(t => t.OITM_Articles)
                .WithMany(t => t.RIN1_ClientCreditNoteDetail)
                .HasForeignKey(d => d.ItemCode);
            this.HasRequired(t => t.ORIN_ClientCreditNotes)
                .WithMany(t => t.RIN1_ClientCreditNoteDetail)
                .HasForeignKey(d => d.IdClientCreditNoteL);

        }
    }
}
