using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class CategoryLink
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

    }
}