using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OIGN_GoodsReceiptMap : EntityTypeConfiguration<OIGN_GoodsReceipt>
    {
        public OIGN_GoodsReceiptMap()
        {
            // Primary Key
            this.HasKey(t => t.IdGoodReceiptL);

            // Properties
            this.Property(t => t.CANCELED)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DocStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Comments)
                .HasMaxLength(254);

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.Ref2)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("OIGN_GoodsReceipt");
            this.Property(t => t.IdGoodReceiptL).HasColumnName("IdGoodReceiptL");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.DocNum).HasColumnName("DocNum");
            this.Property(t => t.CANCELED).HasColumnName("CANCELED");
            this.Property(t => t.DocStatus).HasColumnName("DocStatus");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.DocDueDate).HasColumnName("DocDueDate");
            this.Property(t => t.DocTotal).HasColumnName("DocTotal");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.Ref2).HasColumnName("Ref2");
            this.Property(t => t.Series).HasColumnName("Series");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.HasToBeSync).HasColumnName("HasToBeSync");

            // Relationships
            this.HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.OIGN_GoodsReceipt)
                .HasForeignKey(d => d.WhsCode);

        }
    }
}
