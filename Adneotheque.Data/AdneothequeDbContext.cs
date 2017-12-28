using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities;
using Adneotheque.Entities.Persistence.EntityConfigurations;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.InteropServices;
using Adneotheque.Entities.Entities;

namespace Adneotheque.Data
{
    public interface IAdneothequeDbContext : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
    }

    public class AdneothequeDbContext : DbContext, IAdneothequeDbContext
    {
        public AdneothequeDbContext(): base("DefaultConnection")
        {
            
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Many to many need configurations ?
            modelBuilder.Entity<Document>()
                .HasMany<Author>(d => d.Authors)
                .WithMany(a => a.Documents)
                .Map(cs =>
                {
                    cs.MapLeftKey("Id");
                    cs.MapRightKey("Id");
                    cs.ToTable("AuthorDocuments");
                });

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

        // Explicit definition meaning that we can only get to this query method
        // through an IAdneothequeDbContext reference.
        IQueryable<T> IAdneothequeDbContext.Query<T>()
        {
            return Set<T>();
        }
    }
}
