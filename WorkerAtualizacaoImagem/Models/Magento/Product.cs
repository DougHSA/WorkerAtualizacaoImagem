using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class Product
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("attribute_set_id")]
        public int AttributeSetId { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("type_id")]
        public string TypeId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("extension_attributes")]
        public ExtensionAttributes ExtensionAttributes { get; set; }

        [JsonProperty("product_links")]
        public List<ProductLink> ProductLinks { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        [JsonProperty("media_gallery_entries")]
        public List<MediaGalleryEntry> MediaGalleryEntries { get; set; }

        [JsonProperty("tier_prices")]
        public List<TierPrice> TierPrices { get; set; }

        [JsonProperty("custom_attributes")]
        public List<CustomAttribute> CustomAttributes { get; set; }

    }
}