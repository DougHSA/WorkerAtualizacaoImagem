using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class FilterGroup
    {
        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }
    }
}