using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.Web.AppStart
{
    public static class AutoMapperServiceExtension
    {
        public static void AddAutoMapperCustom(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TestTask.Web.MappingProfile());
                mc.AddProfile(new TestTask.Services.MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
