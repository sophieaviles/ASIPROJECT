using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UColor_topMap : EntityTypeConfiguration<UColor_top>
    {
        public UColor_topMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.LineId });

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Object)
                .HasMaxLength(20);

            this.Property(t => t.U_Nombre)
                .HasMaxLength(51);

            this.Property(t => t.U_Estado)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("UColor_top");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.LineId).HasColumnName("LineId");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.LogInst).HasColumnName("LogInst");
            this.Property(t => t.U_Nombre).HasColumnName("U_Nombre");
            this.Property(t => t.U_Estado).HasColumnName("U_Estado");
        }
    }
}
