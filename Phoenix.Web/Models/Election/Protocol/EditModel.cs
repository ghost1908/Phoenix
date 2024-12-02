using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election.Protocol
{
    public class ProtocolEditModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("items")]
        public List<ProtocolItemEditModel> Items { get; set; }

        [JsonPropertyName("issue")]
        public string Issue { get; set; }
    }

    public class ProtocolItemEditModel
    {
        [JsonPropertyName("itemId")]
        public string ItemId { get; set; }

        [JsonPropertyName("itemValue")]
        public long? ItemValue { get; set; }
    }
}
