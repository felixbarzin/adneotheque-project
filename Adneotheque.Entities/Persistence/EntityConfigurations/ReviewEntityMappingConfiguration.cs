using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Entities;

namespace Adneotheque.Entities.Persistence.EntityConfigurations
{
    public class ReviewEntityMappingConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewEntityMappingConfiguration()
        {
            ToTable("Reviews");

            HasKey(r => r.Id);

            Property(r => r.Body)
                .IsRequired()
                .HasMaxLength(500);

            //Navigation properties

            HasRequired(r => r.Document)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DocumentId);
        }
    }
}
