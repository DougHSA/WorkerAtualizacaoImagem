using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class SearchCriteria
    {

        [JsonProperty("filter_groups")]
        public List<FilterGroup> FilterGroups { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }
    }
}