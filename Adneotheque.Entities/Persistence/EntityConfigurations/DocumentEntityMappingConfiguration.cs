using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Entities;

namespace Adneotheque.Entities.Persistence.EntityConfigurations
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

            Property(d => d.DocumentCategories)
                .IsRequired();

            Property(d => d.DocumentIdentifier)
                .IsRequired();

            Property(d => d.Available)
                .IsRequired();

            Property(d => d.DayAdded)
                .IsRequired();

            Property(d => d.DayBorrowed)
                .IsOptional();

            Property(d => d.BorrowedCounter)
                .IsOptional();

            Property(d => d.DocumentLangage)
                .IsRequired();

            Property(d => d.Pages)
                .IsOptional();

            Property(d => d.Summary)
                .IsOptional();

            //Navigation properties
            HasMany(d => d.Authors)
                .WithMany(d => d.Documents);

            HasMany(d => d.Reviews)
                .WithRequired(r => r.Document)
                .HasForeignKey(r => r.DocumentId)
            .WillCascadeOnDelete(true);


            ////Many to many need configurations ?
            //modelBuilder.Entity<Student>()
            //    .HasMany<Course>(s => s.Courses)
            //    .WithMany(c => c.Students)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("StudentRefId");
            //        cs.MapRightKey("CourseRefId");
            //        cs.ToTable("StudentCourse");
            //    });
        }
    }
}
