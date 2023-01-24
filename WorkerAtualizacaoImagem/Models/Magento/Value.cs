using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class Value
    {

        [JsonProperty("value_index")]
        public int ValueIndex { get; set; }
    }
}