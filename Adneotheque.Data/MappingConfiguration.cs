using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities;
using Adneotheque.Entities.Entities;
using Adneotheque.ViewModels;

namespace Adneotheque.Data
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Document, DocumentViewModel>();
                config.CreateMap<DocumentViewModel, Document>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .ForMember(dest => dest.Reviews, opt => opt.Ignore());
                    //.ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.Available))
                    //.ForMember(dest => dest.DocumentCategories, opt => opt.MapFrom(src => src.DocumentCategories))
                    //.ForMember(dest => dest.DocumentIdentifier, opt => opt.MapFrom(src => src.DocumentIdentifier))
                    //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

                config.CreateMap<ReviewViewModel, Review>();

                config.CreateMap<AuthorViewModel, Author>();
            });
        }
    }
}
