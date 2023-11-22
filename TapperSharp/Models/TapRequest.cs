using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TapperSharp.Models
{
    public class TapRequest
    {
        [JsonPropertyName("func")]
        public string? Func { get; set; }

        [JsonPropertyName("args")]
        public string[]? Args { get; set; }

        [JsonPropertyName("call_id")]
        public string? CallId { get; set; }
    }
}
