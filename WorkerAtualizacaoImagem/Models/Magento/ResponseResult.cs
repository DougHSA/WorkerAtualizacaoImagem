using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkerAtualizacaoImagem.Models.Magento
{
    public class ResponseResult<T> where T : class
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("search_criteria")]
        public SearchCriteria SearchCriteria { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }
    }

    public class ResultProductJson
    {
        [JsonProperty("product")]
        public object Product { get; set; }
    }
    public class ResultOrderJson
    {
        [JsonProperty("entity")]
        public object Entity { get; set; }
    }
}