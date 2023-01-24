using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class Option
    {

        [JsonProperty("product_sku")]
        public string ProductSku { get; set; }

        [JsonProperty("option_id")]
        public int OptionId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }

        [JsonProperty("is_require")]
        public bool IsRequire { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("price_type")]
        public string PriceType { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("file_extension")]
        public string FileExtension { get; set; }

        [JsonProperty("max_characters")]
        public int MaxCharacters { get; set; }

        [JsonProperty("image_size_x")]
        public int ImageSizeX { get; set; }

        [JsonProperty("image_size_y")]
        public int ImageSizeY { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }
    }
}