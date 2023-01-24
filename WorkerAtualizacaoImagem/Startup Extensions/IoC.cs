using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerAtualizacaoImagem.Repository;
using WorkerAtualizacaoImagem.Repository.Interfaces;
using WorkerAtualizacaoImagem.Services;
using WorkerAtualizacaoImagem.Services.Interfaces;

namespace WorkerAtualizacaoImagem.Startup_Extensions
{
    public static class IoC
    {
        public static IServiceCollection AddIoCExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IImageRepository, ImageRepository>();
            services.AddSingleton<IImageService, ImageService>();
            services.AddSingleton<IImageMagentoRepository, ImageMagentoRepository>();
            services.AddSingleton<IImageMagentoService, ImageMagentoService>();
            services.AddSingleton<IMagentoConnection, MagentoConnection>();
            return services;
        }
    }
}
