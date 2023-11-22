using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TapperSharp.Models
{
    public class TapResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("func")]
        public string Func { get; set; }

        [JsonPropertyName("args")]
        public List<string> Args { get; set; }

        [JsonPropertyName("call_id")]
        public string CallId { get; set; }

        [JsonPropertyName("result")]
        public object Result { get; set; }
    }

    public class DeyplomentResult
    {
        [JsonPropertyName("tick")]
        public string Tick { get; set; }

        [JsonPropertyName("max")]
        public string Max { get; set; }

        [JsonPropertyName("lim")]
        public string Lim { get; set; }

        [JsonPropertyName("dec")]
        public int Dec { get; set; }

        [JsonPropertyName("blck")]
        public int Blck { get; set; }

        [JsonPropertyName("tx")]
        public string Tx { get; set; }

        [JsonPropertyName("ins")]
        public string Ins { get; set; }

        [JsonPropertyName("num")]
        public long Num { get; set; }

        [JsonPropertyName("ts")]
        public long Ts { get; set; }

        [JsonPropertyName("addr")]
        public string Addr { get; set; }

        [JsonPropertyName("crsd")]
        public bool Crsd { get; set; }
    }

    public class DeploymentsLengthResult
    {

    }
}
