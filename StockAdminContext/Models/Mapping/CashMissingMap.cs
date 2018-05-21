using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class CashMissingMap : EntityTypeConfiguration<CashMissing>
    {
        public CashMissingMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCashMissing);

            // Properties
            this.Property(t => t.CardCode)
                .HasMaxLength(15);

            this.Property(t => t.Comments)
                .HasMaxLength(254);

            this.Property(t => t.UserAuthorization)
                .IsRequired();

            this.Property(t => t.StateL)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CashMissing");
            this.Property(t => t.IdCashMissing).HasColumnName("IdCashMissing");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.CardCode).HasColumnName("CardCode");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.UserAuthorization).HasColumnName("UserAuthorization");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
        }
    }
}
