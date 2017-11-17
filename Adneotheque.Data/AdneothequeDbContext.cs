using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities;
using Adneotheque.Entities.EntityConfigurations;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;

namespace Adneotheque.Data
{
    public class AdneothequeDbContext : DbContext
    {
        public AdneothequeDbContext(): base("DefaultConnection")
        {
            
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Dynamically load EF Code First Configurations
            var typesToRegister = Assembly.GetAssembly(typeof(AuthorEntityMappingConfiguration)).GetTypes()
                .Where(type => type.Namespace != null && type.Namespace.Equals(typeof(AuthorEntityMappingConfiguration).Namespace))
                .Where(type => type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
            #endregion

        }
    }
}
