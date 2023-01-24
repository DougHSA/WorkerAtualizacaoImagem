using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class ConfigurableProductOption
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("attribute_id")]
        public string AttributeId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("is_use_default")]
        public bool IsUseDefault { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }

        [JsonProperty("product_id")]
        public int ProductId { get; set; }
    }
}