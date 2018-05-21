using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UEspecialtyMap : EntityTypeConfiguration<UEspecialty>
    {
        public UEspecialtyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.DocEntry });

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.DocEntry)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Canceled)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Object)
                .HasMaxLength(20);

            this.Property(t => t.Transfered)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DataSource)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.U_NOMBRE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UEspecialty");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.Canceled).HasColumnName("Canceled");
            this.Property(t => t.Object).HasColumnName("Object");
            this.Property(t => t.LogInst).HasColumnName("LogInst");
            this.Property(t => t.UserSign).HasColumnName("UserSign");
            this.Property(t => t.Transfered).HasColumnName("Transfered");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
            this.Property(t => t.DataSource).HasColumnName("DataSource");
            this.Property(t => t.U_NOMBRE).HasColumnName("U_NOMBRE");
        }
    }
}
