using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities;
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
            });
        }
    }
}
