using System.Net.Http;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMagentoConnection
    {
        Task<string> GetToken();
        Task<string> GET(string endpoint);
        Task<string> POST(string endpoint, HttpContent content);
        Task<string> PUT(string endpoint, HttpContent content);
        Task<string> DELETE(string endpoint);
    }
}