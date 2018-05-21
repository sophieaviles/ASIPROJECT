using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class CheckbookMap : EntityTypeConfiguration<Checkbook>
    {
        public CheckbookMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCheckbookL);

            // Properties
            this.Property(t => t.SerieL)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.StateL)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            this.Property(t => t.CreatedByL)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Checkbook");
            this.Property(t => t.IdCheckbookL).HasColumnName("IdCheckbookL");
            this.Property(t => t.DateL).HasColumnName("DateL");
            this.Property(t => t.SerieL).HasColumnName("SerieL");
            this.Property(t => t.InitialNumL).HasColumnName("InitialNumL");
            this.Property(t => t.NextNumberL).HasColumnName("NextNumberL");
            this.Property(t => t.LastNumL).HasColumnName("LastNumL");
            this.Property(t => t.StateL).HasColumnName("StateL");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");

            // Relationships
            this.HasMany(t => t.NNM1_Series)
                .WithMany(t => t.Checkbooks)
                .Map(m =>
                    {
                        m.ToTable("SeriesCheckbook");
                        m.MapLeftKey("IdCheckbookL");
                        m.MapRightKey("Series");
                    });


        }
    }
}
