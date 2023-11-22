using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TapperSharp.Models
{
    /// <summary>
    /// The TAP Request
    /// </summary>
    public class TapRequest
    {
        /// <summary>
        /// The function
        /// </summary>
        [JsonPropertyName("func")]
        public string? Func { get; set; }

        /// <summary>
        /// The arguments to pass into the function
        /// </summary>
        [JsonPropertyName("args")]
        public object[]? Args { get; set; }

        /// <summary>
        /// The unique call id, used to identify in the response back
        /// </summary>
        [JsonPropertyName("call_id")]
        public string? CallId { get; set; }
    }
}
