using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class ProductLink
    {

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("link_type")]
        public string Link_Type { get; set; }

        [JsonProperty("linked_product_sku")]
        public string Linked_Product_Sku { get; set; }

        [JsonProperty("linked_product_type")]
        public string Linked_Product_Type { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }
    }
}