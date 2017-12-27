using System.Security.Cryptography.X509Certificates;
using Adneotheque.Entities;
using Adneotheque.Entities.Entities;
using Adneotheque.Entities.Enums;
using Bogus;
using FizzWare.NBuilder;
using Faker = Bogus.Faker;

namespace Adneotheque.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<AdneothequeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AdneothequeDbContext context)
        {
            var authorsFaker = new Faker<Author>()
                .RuleFor(a => a.Id, fa => fa.IndexFaker)
                .RuleFor(a => a.Firstname, fa => fa.Name.FirstName())
                .RuleFor(a => a.Lastname, fa => fa.Name.LastName());

            var authors = authorsFaker.Generate(5);

            Random rnd = new Random();

            Array values = Enum.GetValues(typeof(DocumentCategories));

            var documentsFaker = new Faker<Document>()
                .RuleFor(d => d.Id, f => f.IndexFaker)
                .RuleFor(d => d.Title, f => f.Lorem.Sentence(2))
                .RuleFor(d => d.DocumentCategories, f => (DocumentCategories)values.GetValue(rnd.Next(values.Length)))
                .RuleFor(d => d.DocumentLangage, f => (DocumentLangage)values.GetValue(rnd.Next(values.Length)))
                .RuleFor(d => d.DocumentIdentifier, f => Guid.NewGuid().ToString())
                .RuleFor(d => d.Available, f => f.Random.Bool())
                .RuleFor(d => d.BorrowedCounter, f => f.Random.Int(0,14))
                .RuleFor(d => d.DayAdded, f => f.Date.Recent())
                .RuleFor(d => d.Authors, f => authors.OrderBy(x => rnd.Next()).Take(rnd.Next(1, 4)).ToList())
                .RuleFor(d => d.Reviews, f => new Faker<Review>()
                    .RuleFor(r => r.Rating, fa => fa.Random.Bool())
                    .RuleFor(r => r.ReviewerName, fa => fa.Name.FullName())
                    .RuleFor(r => r.Id, fa => fa.IndexFaker)
                    .RuleFor(r => r.Body, fa => fa.Lorem.Paragraph()).Generate(rnd.Next(1,10)))
                .FinishWith((f, d) => 
                {
                    if (!d.Available)
                    {
                        d.DayBorrowed = f.Date.Recent();
                    }
                });
                
            var documents = documentsFaker.Generate(20);

            //foreach(var item in documents){
            //    if(item.Available)
            //        item.DayBorrowed = DateTime.
            //}

            try
            {
                foreach (var item in documents)
                {
                    context.Documents.AddOrUpdate(c => c.Id, item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }
    }
}
