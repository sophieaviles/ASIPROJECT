using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OCRD_BusinessPartnerMap : EntityTypeConfiguration<OCRD_BusinessPartner>
    {
        public OCRD_BusinessPartnerMap()
        {
            // Primary Key
            this.HasKey(t => t.IdBusinessPartner);

            // Properties
            this.Property(t => t.CardCode)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.CardName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.AddId)
                .HasMaxLength(18);

            this.Property(t => t.Notes)
                .HasMaxLength(100);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.CardType)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Phone1)
                .HasMaxLength(20);

            this.Property(t => t.Phone2)
                .HasMaxLength(20);

            this.Property(t => t.Cellular)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.ValidFor)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.U_NIT)
                .HasMaxLength(17);

            // Table & Column Mappings
            this.ToTable("OCRD_BusinessPartner");
            this.Property(t => t.IdBusinessPartner).HasColumnName("IdBusinessPartner");
            this.Property(t => t.CardCode).HasColumnName("CardCode");
            this.Property(t => t.CardName).HasColumnName("CardName");
            this.Property(t => t.GroupCode).HasColumnName("GroupCode");
            this.Property(t => t.AddId).HasColumnName("AddId");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.CardType).HasColumnName("CardType");
            this.Property(t => t.Phone1).HasColumnName("Phone1");
            this.Property(t => t.Phone2).HasColumnName("Phone2");
            this.Property(t => t.Cellular).HasColumnName("Cellular");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.ValidFor).HasColumnName("ValidFor");
            this.Property(t => t.ValidFrom).HasColumnName("ValidFrom");
            this.Property(t => t.ValidTo).HasColumnName("ValidTo");
            this.Property(t => t.U_NIT).HasColumnName("U_NIT");
        }
    }
}
