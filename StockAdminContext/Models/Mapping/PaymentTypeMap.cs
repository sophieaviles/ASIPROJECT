using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class PaymentTypeMap : EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPaymentType);

            // Properties
            this.Property(t => t.PaymentName)
                .IsRequired();

            this.Property(t => t.AcctCode)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("PaymentTypes");
            this.Property(t => t.IdPaymentType).HasColumnName("IdPaymentType");
            this.Property(t => t.PaymentName).HasColumnName("PaymentName");
            this.Property(t => t.AcctCode).HasColumnName("AcctCode");
            this.Property(t => t.IsRoyalty).HasColumnName("IsRoyalty");
            this.Property(t => t.PaymentTypesIdPaymentType).HasColumnName("PaymentTypesIdPaymentType");

            // Relationships
            this.HasRequired(t => t.PaymentType1)
                .WithMany(t => t.PaymentTypes1)
                .HasForeignKey(d => d.PaymentTypesIdPaymentType);

        }
    }
}
