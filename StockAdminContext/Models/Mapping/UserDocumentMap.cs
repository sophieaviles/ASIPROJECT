using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class UserDocumentMap : EntityTypeConfiguration<UserDocument>
    {
        public UserDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ObjType, t.UserId });

            // Properties
            this.Property(t => t.ObjType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TypeAccess)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("UserDocument");
            this.Property(t => t.ObjType).HasColumnName("ObjType");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TypeAccess).HasColumnName("TypeAccess");

            // Relationships
            this.HasRequired(t => t.Document)
                .WithMany(t => t.UserDocuments)
                .HasForeignKey(d => d.ObjType);
            this.HasRequired(t => t.LocalUser)
                .WithMany(t => t.UserDocuments)
                .HasForeignKey(d => d.UserId);

        }
    }
}
