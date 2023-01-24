using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkerAtualizacaoImagem;
using WorkerAtualizacaoImagem.Startup_Extensions;


namespace WorkerAtualizacaoImagem
{
    public class Program
    {

        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddHostedService<Worker>();
                    services.AddIoCExtensions();
                    services.AddSessionsServiceExtensions(configuration);
                })
                .Build();

            host.Run();
        }
    }
}

