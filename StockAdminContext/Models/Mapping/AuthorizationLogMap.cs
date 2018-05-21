using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class AuthorizationLogMap : EntityTypeConfiguration<AuthorizationLog>
    {
        public AuthorizationLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAuthorizationLog);

            // Properties
            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("AuthorizationLog");
            this.Property(t => t.IdAuthorizationLog).HasColumnName("IdAuthorizationLog");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.DocEntry).HasColumnName("DocEntry");
            this.Property(t => t.Date).HasColumnName("Date");
        }
    }
}
