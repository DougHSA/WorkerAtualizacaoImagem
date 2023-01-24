using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class CustomAttribute
    {
        [JsonProperty("attribute_code")]
        public string AttributeCode { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}