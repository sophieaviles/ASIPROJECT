using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UserAuthorizationSetMap : EntityTypeConfiguration<UserAuthorizationSet>
    {
        public UserAuthorizationSetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ObjType, t.UserId });

            // Properties
            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("UserAuthorizationSet");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LocalUsers_UserId).HasColumnName("LocalUsers_UserId");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.UserAuthorizationSets)
                .HasForeignKey(d => d.ObjType);
            this.HasOptional(t => t.LocalUser)
                .WithMany(t => t.UserAuthorizationSets)
                .HasForeignKey(d => d.LocalUsers_UserId);

        }
    }
}
