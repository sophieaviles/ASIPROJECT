using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OWTR_TransfersMap : EntityTypeConfiguration<OWTR_Transfers>
    {
        public OWTR_TransfersMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTransferL);

            // Properties
            this.Property(t => t.CANCELED)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DocStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CardCode)
                .HasMaxLength(15);

            this.Property(t => t.Comments)
                .HasMaxLength(254);

            this.Property(t => t.JrnlMemo)
                .HasMaxLength(50);

            this.Property(t => t.Filler)
                .HasMaxLength(8);

            this.Property(t => t.ObjType)
                .HasMaxLength(20);

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            this.Property(t => t.IdTransferL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OWTR_Transfers");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.DocNum).HasColumnName("DocNum");
            this.Property(t => t.CANCELED).HasColumnName("CANCELED");
            this.Property(t => t.DocStatus).HasColumnName("DocStatus");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.DocDueDate).HasColumnName("DocDueDate");
            this.Property(t => t.CardCode).HasColumnName("CardCode");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.JrnlMemo).HasColumnName("JrnlMemo");
            this.Property(t => t.Series).HasColumnName("Series");
            this.Property(t => t.TaxDate).HasColumnName("TaxDate");
            this.Property(t => t.Filler).HasColumnName("Filler");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.DeliveryDateL).HasColumnName("DeliveryDateL");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.LastSyncDateL).HasColumnName("LastSyncDateL");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.DocTotal).HasColumnName("DocTotal");
            this.Property(t => t.ReceptionDateL).HasColumnName("ReceptionDateL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.HasToBeSync).HasColumnName("HasToBeSync");
            this.Property(t => t.IdTransferL).HasColumnName("IdTransferL");

            // Relationships
            this.HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OWTR_Transfers)
                .HasForeignKey(d => d.WhsCode);

        }
    }
}
