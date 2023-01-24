using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class Filter
    {
        [JsonProperty("field")]
        public string field { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("condition_type")]
        public string condition_type { get; set; }
    }
}