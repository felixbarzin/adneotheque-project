using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Entities;

namespace Adneotheque.Entities.Persistence.EntityConfigurations
{
    public class AuthorEntityMappingConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorEntityMappingConfiguration()
        {
            ToTable("Authors");

            HasKey(a => a.Id);

            Property(a => a.Firstname)
                .IsRequired()
                .HasMaxLength(55);

            Property(a => a.Lastname)
                .HasMaxLength(67);

            //Navigation properties

            HasMany(a => a.Documents)
                .WithMany(d => d.Authors);
        }
    }
}
