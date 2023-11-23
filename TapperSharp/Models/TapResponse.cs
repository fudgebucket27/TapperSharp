using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TapperSharp.Models
{
    /// <summary>
    /// The generic TAP response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TapResponse<T>
    {
        /// <summary>
        /// The error
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        /// <summary>
        /// The function
        /// </summary>
        [JsonPropertyName("func")]
        public string? Func { get; set; }

        /// <summary>
        /// The arguments that were passed in
        /// </summary>
        [JsonPropertyName("args")]
        public List<object?>? Args { get; set; }

        /// <summary>
        /// The unique call id
        /// </summary>
        [JsonPropertyName("call_id")]
        public string? CallId { get; set; }

        /// <summary>
        /// The result, is Generic to handle the different response back
        /// </summary>
        [JsonPropertyName("result")]
        public T? Result { get; set; }
    }

    /// <summary>
    /// The base TAP response, used only to get the function so that it can be converted into the generic TAP response
    /// </summary>
    public class TapResponseBase
    {
        /// <summary>
        /// The error
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        /// <summary>
        /// The function
        /// </summary>
        [JsonPropertyName("func")]
        public string? Func { get; set; }

        /// <summary>
        /// The arguments that were passed in
        /// </summary>
        [JsonPropertyName("args")]
        public List<object?>? Args { get; set; }

        /// <summary>
        /// The unique call id
        /// </summary>
        [JsonPropertyName("call_id")]
        public string? CallId { get; set; }
    }

    /// <summary>
    /// The deployment result for a ticker
    /// </summary>
    public class DeploymentResult
    {
        /// <summary>
        /// The ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }

        /// <summary>
        /// The max amount
        /// </summary>
        [JsonPropertyName("max")]
        public string? Max { get; set; }

        /// <summary>
        /// The limit
        /// </summary>
        [JsonPropertyName("lim")]
        public string? Lim { get; set; }

        /// <summary>
        /// The amount of decimals
        /// </summary>
        [JsonPropertyName("dec")]
        public int Dec { get; set; }

        /// <summary>
        /// The block
        /// </summary>
        [JsonPropertyName("blck")]
        public int Blck { get; set; }

        /// <summary>
        /// The transaction
        /// </summary>
        [JsonPropertyName("tx")]
        public string? Tx { get; set; }

        /// <summary>
        /// The inscription
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }

        /// <summary>
        /// The number
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }

        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public long Ts { get; set; }

        /// <summary>
        /// The address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }

        /// <summary>
        /// True/false for if cursed
        /// </summary>
        [JsonPropertyName("crsd")]
        public bool Crsd { get; set; }
    }

    /// <summary>
    /// The holders result for a ticker
    /// </summary>
    public class HoldersResult
    {
        /// <summary>
        /// The holder address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// The balance in big integer format
        /// </summary>
        [JsonPropertyName("balance")]
        public string? Balance { get; set; }
        /// <summary>
        /// The transferable amount
        /// </summary>
        [JsonPropertyName("transferable")]
        public object? Transferable { get; set; }
    }

    /// <summary>
    /// The mint list result
    /// </summary>
    public class MintListResult
    {
        /// <summary>
        /// The address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }
        /// <summary>
        /// The block
        /// </summary>
        [JsonPropertyName("blck")]
        public long Blck { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
        /// <summary>
        /// The balance
        /// </summary>
        [JsonPropertyName("bal")]
        public string? Bal { get; set; }
        /// <summary>
        /// The transaction
        /// </summary>
        [JsonPropertyName("tx")]
        public string? Tx { get; set; }
        /// <summary>
        /// The inscription
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }
        /// <summary>
        /// The number
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }
        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public long Ts { get; set; }
        /// <summary>
        /// Bool for if mint has failed
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
    }

}
