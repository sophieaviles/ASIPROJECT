using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class LocalUserMap : EntityTypeConfiguration<LocalUser>
    {
        public LocalUserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.NickName)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Password)
                .IsRequired();

            this.Property(t => t.Position)
                .IsRequired();

            this.Property(t => t.Locked)
                .IsRequired();

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LocalUsers");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.NickName).HasColumnName("NickName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Locked).HasColumnName("Locked");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
            this.Property(t => t.AllowChangePrices).HasColumnName("AllowChangePrices");
            this.Property(t => t.AllowProcessing).HasColumnName("AllowProcessing");
        }
    }
}
