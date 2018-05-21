using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OWHS_BranchMap : EntityTypeConfiguration<OWHS_Branch>
    {
        public OWHS_BranchMap()
        {
            // Primary Key
            this.HasKey(t => t.WhsCode);

            // Properties
            this.Property(t => t.WhsCode)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.WhsName)
                .HasMaxLength(100);

            this.Property(t => t.Grp_Code)
                .HasMaxLength(4);

            this.Property(t => t.Locked)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OWHS_Branch");
            this.Property(t => t.WhsCode).HasColumnName("WhsCode");
            this.Property(t => t.WhsName).HasColumnName("WhsName");
            this.Property(t => t.Grp_Code).HasColumnName("Grp_Code");
            this.Property(t => t.Locked).HasColumnName("Locked");
            this.Property(t => t.LockedForOrderL).HasColumnName("LockedForOrderL");
        }
    }
}
