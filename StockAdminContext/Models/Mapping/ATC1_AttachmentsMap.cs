using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class ATC1_AttachmentsMap : EntityTypeConfiguration<ATC1_Attachments>
    {
        public ATC1_AttachmentsMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAtached);

            // Properties
            this.Property(t => t.srcPath)
                .IsRequired();

            this.Property(t => t.trgtPath)
                .IsRequired();

            this.Property(t => t.FileName)
                .IsRequired();

            this.Property(t => t.FileExt)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Copied)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Override)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ATC1_Attachments");
            this.Property(t => t.IdAtached).HasColumnName("IdAtached");
            this.Property(t => t.AbsEntry).HasColumnName("AbsEntry");
            this.Property(t => t.Line).HasColumnName("Line");
            this.Property(t => t.srcPath).HasColumnName("srcPath");
            this.Property(t => t.trgtPath).HasColumnName("trgtPath");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileExt).HasColumnName("FileExt");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.UsrID).HasColumnName("UsrID");
            this.Property(t => t.Copied).HasColumnName("Copied");
            this.Property(t => t.Override).HasColumnName("Override");
            this.Property(t => t.IdEspecialOrder).HasColumnName("IdEspecialOrder");

            // Relationships
            this.HasRequired(t => t.ORDR_SpecialOrder)
                .WithMany(t => t.ATC1_Attachments)
                .HasForeignKey(d => d.IdEspecialOrder);

        }
    }
}
