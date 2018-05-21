using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UMovements_AccMap : EntityTypeConfiguration<UMovements_Acc>
    {
        public UMovements_AccMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.LineId, t.U_codigo, t.U_ITEMCODE });

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Object)
                .HasMaxLength(20);

            this.Property(t => t.U_AcctCode)
                .HasMaxLength(10);

            this.Property(t => t.U_codigo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.U_ITEMCODE)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("UMovements_Acc");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.LineId).HasColumnName("LineId");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.LogInst).HasColumnName("LogInst");
            this.Property(t => t.U_AcctCode).HasColumnName("U_AcctCode");
            this.Property(t => t.U_codigo).HasColumnName("U_codigo");
            this.Property(t => t.U_ITEMCODE).HasColumnName("U_ITEMCODE");
        }
    }
}
