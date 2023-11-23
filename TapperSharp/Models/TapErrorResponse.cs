using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TapperSharp.Models
{
    public class Cmd
    {
        /// <summary>
        /// The function
        /// </summary>
        [JsonPropertyName("func")]
        public string? func { get; set; }
        /// <summary>
        /// The arguments
        /// </summary>
        [JsonPropertyName("args")]
        public List<object>? Args { get; set; }
        /// <summary>
        /// The call id
        /// </summary>
        [JsonPropertyName("call_id")]
        public string? CallId { get; set; }
    }

    /// <summary>
    /// The TAP error response
    /// </summary>
    public class TapErrorResponse
    {
        /// <summary>
        /// The error
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        /// <summary>
        /// The command
        /// </summary>
        [JsonPropertyName("cmd")]
        public Cmd? Cmd { get; set; }
    }
}
