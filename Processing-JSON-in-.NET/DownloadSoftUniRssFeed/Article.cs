using System;
using Newtonsoft.Json;

namespace DownloadSoftUniRssFeed
{
    public class Article
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("a10:updated")]
        public DateTime UpdatedDate { get; set; }
    }
}
