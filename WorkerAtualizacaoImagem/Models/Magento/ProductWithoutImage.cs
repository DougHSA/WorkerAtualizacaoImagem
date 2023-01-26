using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class ProductWithoutImage
    {

        [JsonProperty("success")]
        public bool Status { get; set; }

        [JsonProperty("skus")]
        public string[] Skus { get; set; }

    }
}