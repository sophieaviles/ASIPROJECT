using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class OITB_GroupsMap : EntityTypeConfiguration<OITB_Groups>
    {
        public OITB_GroupsMap()
        {
            // Primary Key
            this.HasKey(t => t.ItmsGrpCod);

            // Properties
            this.Property(t => t.ItmsGrpCod)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ItmsGrpNam)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Locked)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.StateL)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.MandatoryCount)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OITB_Groups");
            this.Property(t => t.ItmsGrpCod).HasColumnName("ItmsGrpCod");
            this.Property(t => t.ItmsGrpNam).HasColumnName("ItmsGrpNam");
            this.Property(t => t.LastSyncDateL).HasColumnName("LastSyncDateL");
            this.Property(t => t.Locked).HasColumnName("Locked");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.MandatoryCount).HasColumnName("MandatoryCount");
        }
    }
}
