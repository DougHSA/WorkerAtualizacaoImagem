using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class TierPrice
    {

        [JsonProperty("customer_group_id")]
        public int CustomerGroupId { get; set; }

        [JsonProperty("qty")]
        public int Qty { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}