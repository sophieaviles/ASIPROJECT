using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UCoverMap : EntityTypeConfiguration<UCover>
    {
        public UCoverMap()
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
                .HasMaxLength(50);

            this.Property(t => t.U_ESTADO)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("UCover");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.LineId).HasColumnName("LineId");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.LogInst).HasColumnName("LogInst");
            this.Property(t => t.U_Nombre).HasColumnName("U_Nombre");
            this.Property(t => t.U_ESTADO).HasColumnName("U_ESTADO");
        }
    }
}
