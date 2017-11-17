using System.Security.Cryptography.X509Certificates;
using Adneotheque.Entities;
using Bogus;
using FizzWare.NBuilder;

namespace Adneotheque.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdneothequeDbContext>
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

            var reviewsFaker = new Faker<Review>()
                .RuleFor(r => r.Id, f => f.IndexFaker)
                .RuleFor(r => r.Body, f => f.Lorem.Paragraph());
            //.RuleFor(r => r.Document, f => documents[rnd.Next(0,documents.Count)]);

            var reviews = reviewsFaker.Generate(25);

            var documentsFaker = new Faker<Document>()
                .RuleFor(d => d.Id, f => f.IndexFaker)
                .RuleFor(d => d.Title, f => f.Lorem.Sentence(2))
                .RuleFor(d => d.Authors, f => authors.OrderBy(x => rnd.Next()).Take(rnd.Next(1, 4)).ToList())
                .RuleFor(d => d.Reviews, f => new Faker<Review>()
                    .RuleFor(r => r.Id, fa => fa.IndexFaker)
                    .RuleFor(r => r.Body, fa => fa.Lorem.Paragraph()).Generate(rnd.Next(1,10)));
                

            var documents = documentsFaker.Generate(10);

            foreach (var item in documents)
            {
                context.Documents.AddOrUpdate(c => c.Id, item);

            }

            

            //___________________________________________________
            //var authorsFaker = new Faker<Author>()
            //    .RuleFor(a => a.Id, fa => fa.IndexFaker)
            //    .RuleFor(a => a.Firstname, fa => fa.Name.FirstName())
            //    .RuleFor(a => a.Lastname, fa => fa.Name.LastName());

            //Random rnd = new Random();

            //var documentsFaker = new Faker<Document>()
            //        .RuleFor(d => d.Id, f => f.IndexFaker)
            //        .RuleFor(d => d.Title, f => f.Lorem.Sentence(2))
            //        .RuleFor(d => d.Authors, f => authorsFaker.Generate(rnd.Next(1,4)).ToList())
            //    ;

            //var documents = documentsFaker.Generate(10);

            //foreach (var item in documents)
            //{
            //    context.Documents.AddOrUpdate(c => c.Id, item);

            //}
            //___________________________________________________

            //var documentsFaker = new Faker<Document>()
            //    .RuleFor(d => d.Id, f => f.IndexFaker)
            //    .RuleFor(d => d.Title, f => f.Lorem.Sentence(2))
            //    .RuleFor(d => d.Authors, f => new Faker<Author>()
            //                            .RuleFor(a => a.Id, fa => fa.IndexFaker)
            //                            .RuleFor(a => a.Firstname, fa => f.Name.FirstName())
            //                            .RuleFor(a => a.Lastname, fa => f.Name.LastName()).Generate(10))
            //    ;
            //var documents = documentsFaker.Generate(10);

            //foreach (var item in documents)
            //{
            //    context.Documents.AddOrUpdate(c => c.Id, item);

            //}
        }
    }
}
