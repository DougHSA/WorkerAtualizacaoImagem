using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                    services.AddLogging(logginBuilder =>
                    {
                        //logginBuilder.AddSeq(configuration.GetSection("Seq"));
                        logginBuilder.AddConfiguration(configuration.GetSection("seq"));
                        logginBuilder.AddSeq("http://168.138.250.55:5341", "yp8AGP25rGbVKP6MMiEp");
                        //logginBuilder.AddSeq();
                    }); 
                })
                .Build();

            host.Run();
        }
    }
}

