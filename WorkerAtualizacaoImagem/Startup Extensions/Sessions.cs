using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerAtualizacaoImagem.Startup_Extensions
{
    public static class Sessions
    {
        public static IServiceCollection AddSessionsServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BrunskerSettings>(configuration.GetSection("BrunskerSettings"));
            return services;
        }
    }
}
