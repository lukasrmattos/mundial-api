using Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Repositories;
using Application.Services;
using InfraData.Repositories;
using InfraData;

namespace InfraIOC
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Auto Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            // UoW
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Repo
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();

            // Services
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IPerfilService, PerfilService>();
        }
    }
}
