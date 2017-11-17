using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.Entities.EntityConfigurations
{
    class DocumentEntityMappingConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentEntityMappingConfiguration()
        {
            ToTable("Documents");

            HasKey(d => d.Id);

            Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(100);

            //Navigation properties

            HasMany(d => d.Authors)
                .WithMany(d => d.Documents);
        }
    }
}
