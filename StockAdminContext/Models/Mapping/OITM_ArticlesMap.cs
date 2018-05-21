using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OITM_ArticlesMap : EntityTypeConfiguration<OITM_Articles>
    {
        public OITM_ArticlesMap()
        {
            // Primary Key
            this.HasKey(t => t.ItemCode);

            // Properties
            this.Property(t => t.ItemCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ItemName)
                .HasMaxLength(100);

            this.Property(t => t.CodeBars)
                .HasMaxLength(16);

            this.Property(t => t.PrchseItem)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SellItem)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.InvntItem)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CardCode)
                .HasMaxLength(15);

            this.Property(t => t.BuyUnitMsr)
                .HasMaxLength(20);

            this.Property(t => t.SalUnitMsr)
                .HasMaxLength(20);

            this.Property(t => t.validFor)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ItemType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.InvntryUom)
                .HasMaxLength(20);

            this.Property(t => t.AssetItem)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.TemplateL)
                .IsRequired();

            this.Property(t => t.OWHS_BranchWhsCode)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.U_LINEA)
                .HasMaxLength(30);

            this.Property(t => t.U_SUBLINEA)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("OITM_Articles");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.ItmsGrpCod).HasColumnName("ItmsGrpCod");
            this.Property(t => t.CodeBars).HasColumnName("CodeBars");
            this.Property(t => t.PrchseItem).HasColumnName("PrchseItem");
            this.Property(t => t.SellItem).HasColumnName("SellItem");
            this.Property(t => t.InvntItem).HasColumnName("InvntItem");
            this.Property(t => t.CardCode).HasColumnName("CardCode");
            this.Property(t => t.BuyUnitMsr).HasColumnName("BuyUnitMsr");
            this.Property(t => t.NumInBuy).HasColumnName("NumInBuy");
            this.Property(t => t.SalUnitMsr).HasColumnName("SalUnitMsr");
            this.Property(t => t.AvgPrice).HasColumnName("AvgPrice");
            this.Property(t => t.PurPackUn).HasColumnName("PurPackUn");
            this.Property(t => t.SalPackUn).HasColumnName("SalPackUn");
            this.Property(t => t.validFor).HasColumnName("validFor");
            this.Property(t => t.validFrom).HasColumnName("validFrom");
            this.Property(t => t.validTo).HasColumnName("validTo");
            this.Property(t => t.ItemType).HasColumnName("ItemType");
            this.Property(t => t.InvntryUom).HasColumnName("InvntryUom");
            this.Property(t => t.NumInSale).HasColumnName("NumInSale");
            this.Property(t => t.AssetItem).HasColumnName("AssetItem");
            this.Property(t => t.LastSyncDateL).HasColumnName("LastSyncDateL");
            this.Property(t => t.TemplateL).HasColumnName("TemplateL");
            this.Property(t => t.OWHS_BranchWhsCode).HasColumnName("OWHS_BranchWhsCode");
            this.Property(t => t.U_LINEA).HasColumnName("U_LINEA");
            this.Property(t => t.U_SUBLINEA).HasColumnName("U_SUBLINEA");

            // Relationships
            this.HasOptional(t => t.OITB_Groups)
                .WithMany(t => t.OITM_Articles)
                .HasForeignKey(d => d.ItmsGrpCod);

        }
    }
}
