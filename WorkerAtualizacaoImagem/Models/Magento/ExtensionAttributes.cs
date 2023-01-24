using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class ExtensionAttributes
    {
        [JsonProperty("website_ids")]
        public List<int> WebsiteIds { get; set; }

        [JsonProperty("category_links")]
        public List<CategoryLink> CategoryLinks { get; set; }

        [JsonProperty("stock_item")]
        public StockItem StockItem { get; set; }

        [JsonProperty("configurable_product_options")]
        public List<ConfigurableProductOption> ConfigurableProductOptions { get; set; }

        [JsonProperty("configurable_product_links")]
        public List<int> ConfigurableProductLinks { get; set; }
    }
}