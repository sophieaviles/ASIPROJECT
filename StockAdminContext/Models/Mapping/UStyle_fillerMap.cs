using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UStyle_fillerMap : EntityTypeConfiguration<UStyle_filler>
    {
        public UStyle_fillerMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.LineId, t.U_NOMBRE });

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.LineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Object)
                .HasMaxLength(20);

            this.Property(t => t.U_NOMBRE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.U_ESTADO)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("UStyle_filler");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.LineId).HasColumnName("LineId");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.LogInst).HasColumnName("LogInst");
            this.Property(t => t.U_NOMBRE).HasColumnName("U_NOMBRE");
            this.Property(t => t.U_ESTADO).HasColumnName("U_ESTADO");
        }
    }
}
