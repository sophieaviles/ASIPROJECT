using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class ORDR_SpecialOrderMap : EntityTypeConfiguration<ORDR_SpecialOrder>
    {
        public ORDR_SpecialOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.IdSpecialOrder);

            // Properties
            this.Property(t => t.DocType)
                .IsRequired();

            this.Property(t => t.CANCELED)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DocStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CardCode)
                .HasMaxLength(15);

            this.Property(t => t.NumAtCard)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Comments)
                .HasMaxLength(254);

            this.Property(t => t.JrnlMemo)
                .HasMaxLength(50);

            this.Property(t => t.Ref2)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.Ref1)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            this.Property(t => t.StateL)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.WhsCode)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("ORDR_SpecialOrder");
            this.Property(t => t.IdSpecialOrder).HasColumnName("IdSpecialOrder");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.AtcEntry).HasColumnName("AtcEntry");
            this.Property(t => t.DocNum).HasColumnName("DocNum");
            this.Property(t => t.DocType).HasColumnName("DocType");
            this.Property(t => t.CANCELED).HasColumnName("CANCELED");
            this.Property(t => t.DocStatus).HasColumnName("DocStatus");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.DocDueDate).HasColumnName("DocDueDate");
            this.Property(t => t.CardCode).HasColumnName("CardCode");
            this.Property(t => t.NumAtCard).HasColumnName("NumAtCard");
            this.Property(t => t.DocTotal).HasColumnName("DocTotal");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.JrnlMemo).HasColumnName("JrnlMemo");
            this.Property(t => t.Series).HasColumnName("Series");
            this.Property(t => t.Ref2).HasColumnName("Ref2");
            this.Property(t => t.Ref1).HasColumnName("Ref1");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.HasToBeSync).HasColumnName("HasToBeSync");

            // Relationships
            this.HasOptional(t => t.OWHS_Branch)
                .WithMany(t => t.ORDR_SpecialOrder)
                .HasForeignKey(d => d.WhsCode);

        }
    }
}
