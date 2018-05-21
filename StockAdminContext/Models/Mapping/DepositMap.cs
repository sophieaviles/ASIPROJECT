using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class DepositMap : EntityTypeConfiguration<Deposit>
    {
        public DepositMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDeposits);

            // Properties
            this.Property(t => t.Cashier)
                .IsRequired()
                .HasMaxLength(100);

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
            this.ToTable("Deposits");
            this.Property(t => t.IdDeposits).HasColumnName("IdDeposits");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Cashier).HasColumnName("Cashier");
            this.Property(t => t.Cash).HasColumnName("Cash");
            this.Property(t => t.Cheque).HasColumnName("Cheque");
            this.Property(t => t.UserAuthorization).HasColumnName("UserAuthorization");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.Number).HasColumnName("Number");
        }
    }
}
