using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class Content
    {
        [JsonProperty("base64_encoded_data")]
        public string Base64EncodedData { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}