using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SigiApi.Models.Mapping
{
    public class ParameterMap : EntityTypeConfiguration<Parameter>
    {
        public ParameterMap()
        {
            // Primary Key
            this.HasKey(t => t.IdParameters);

            // Properties
            this.Property(t => t.Parameter1)
                .IsRequired();

            this.Property(t => t.Value1)
                .IsRequired();

            this.Property(t => t.Value2)
                .IsRequired();

            this.Property(t => t.CreatedByL)
                .IsRequired();

            this.Property(t => t.ModifiedByL)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Parameters");
            this.Property(t => t.IdParameters).HasColumnName("IdParameters");
            this.Property(t => t.Parameter1).HasColumnName("Parameter");
            this.Property(t => t.Value1).HasColumnName("Value1");
            this.Property(t => t.Value2).HasColumnName("Value2");
            this.Property(t => t.CreatedDateL).HasColumnName("CreatedDateL");
            this.Property(t => t.CreatedByL).HasColumnName("CreatedByL");
            this.Property(t => t.ModifiedByL).HasColumnName("ModifiedByL");
            this.Property(t => t.ModifiedDateL).HasColumnName("ModifiedDateL");
        }
    }
}
