using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Interfaces;
using WorkerAtualizacaoImagem.Models.Magento;

namespace Repository
{
    public class MagentoConnection : IMagentoConnection
    {
        protected BrunskerSettings _settings { get; }
        private readonly ILogger<MagentoConnection> _logger;

        public MagentoConnection(IOptions<BrunskerSettings> settings, ILogger<MagentoConnection> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task<string> GetToken()
        {
            try
            {
                Credential user = new Credential { username = _settings.Username, password = _settings.Password };

                var data = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(_settings.Url + "rest/V1/integration/admin/token", data);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        return result.Substring(1, 32);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
        public async Task<string> GET(string endpoint)
        {
            try
            {
                string token = await GetToken();

                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync(_settings.Url + endpoint);

                    var result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                    var x = result.ToString();

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception (x.Substring(x.IndexOf("\"message") ,(x.IndexOf(".")- x.IndexOf("\"message") + 1)).Replace("\"",""));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
        public async Task<string> POST(string endpoint, HttpContent content)
        {
            try
            {
                string token = await GetToken();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(_settings.Url + endpoint, content);

                    var result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                    var x = result.ToString();

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception (x.Substring(x.IndexOf("\"message") ,(x.IndexOf(".")- x.IndexOf("\"message") + 1)).Replace("\"",""));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
        public async Task<string> PUT(string endpoint, HttpContent content)
        {
            try
            {
                string token = await GetToken();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PutAsync(_settings.Url + endpoint, content);

                    var result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                    var x = result.ToString();

                    if (response.IsSuccessStatusCode)
                    {
                        var result1 = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception (x.Substring(x.IndexOf("\"message") ,(x.IndexOf(".")- x.IndexOf("\"message") + 1)).Replace("\"",""));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
        public async Task<string> DELETE(string endpoint)
        {
            try
            {
                string token = await GetToken();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.DeleteAsync(_settings.Url + endpoint);

                    var result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                    var x = result.ToString();

                    if (response.IsSuccessStatusCode)
                    {
                        result = JsonConvert.DeserializeObject<JToken>(await response.Content.ReadAsStringAsync());

                        return result.ToString();
                    }
                    else
                    {
                        throw new Exception (x.Substring(x.IndexOf("\"message") ,(x.IndexOf(".")- x.IndexOf("\"message") + 1)).Replace("\"",""));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }
    }
}