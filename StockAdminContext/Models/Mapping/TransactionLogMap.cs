using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class TransactionLogMap : EntityTypeConfiguration<TransactionLog>
    {
        public TransactionLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTransactionLog);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.State)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.CreatedBy)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("TransactionLog");
            this.Property(t => t.IdTransactionLog).HasColumnName("IdTransactionLog");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.VerificationCode).HasColumnName("VerificationCode");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
        }
    }
}
