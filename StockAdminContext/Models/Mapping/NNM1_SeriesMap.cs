using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class NNM1_SeriesMap : EntityTypeConfiguration<NNM1_Series>
    {
        public NNM1_SeriesMap()
        {
            // Primary Key
            this.HasKey(t => t.Series);

            // Properties
            this.Property(t => t.ObjectCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Series)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SeriesName)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Remark)
                .HasMaxLength(50);

            this.Property(t => t.Locked)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.StateL)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NNM1_Series");
            this.Property(t => t.ObjectCode).HasColumnName("ObjectCode");
            this.Property(t => t.Series).HasColumnName("Series");
            this.Property(t => t.SeriesName).HasColumnName("SeriesName");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Locked).HasColumnName("Locked");
            this.Property(t => t.StateL).HasColumnName("StateL");
        }
    }
}
