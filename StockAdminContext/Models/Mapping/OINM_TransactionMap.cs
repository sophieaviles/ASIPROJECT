using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OINM_TransactionMap : EntityTypeConfiguration<OINM_Transaction>
    {
        public OINM_TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTransaction);

            // Properties
            this.Property(t => t.Ref1)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.Ref2)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.Comments)
                .IsRequired()
                .HasMaxLength(254);

            this.Property(t => t.JrnMemo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ItemCode)
                .HasMaxLength(20);

            this.Property(t => t.Dscription)
                .HasMaxLength(100);

            this.Property(t => t.SerialNum)
                .IsRequired()
                .HasMaxLength(17);

            this.Property(t => t.WareHouse)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("OINM_Transaction");
            this.Property(t => t.IdTransaction).HasColumnName("IdTransaction");
            this.Property(t => t.TransNum).HasColumnName("TransNum");
            this.Property(t => t.TransType).HasColumnName("TransType");
            this.Property(t => t.DocDate).HasColumnName("DocDate");
            this.Property(t => t.DocDueDate).HasColumnName("DocDueDate");
            this.Property(t => t.Ref1).HasColumnName("Ref1");
            this.Property(t => t.Ref2).HasColumnName("Ref2");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.JrnMemo).HasColumnName("JrnMemo");
            this.Property(t => t.DocTime).HasColumnName("DocTime");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.Dscription).HasColumnName("Dscription");
            this.Property(t => t.InQty).HasColumnName("InQty");
            this.Property(t => t.OutQty1).HasColumnName("OutQty1");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.SerialNum).HasColumnName("SerialNum");
            this.Property(t => t.WareHouse).HasColumnName("WareHouse");
            this.Property(t => t.HasToBeSync).HasColumnName("HasToBeSync");
        }
    }
}
