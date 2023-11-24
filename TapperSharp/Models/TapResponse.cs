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
        public long Blck { get; set; }

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
        /// The ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }

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

    /// <summary>
    /// The transfer list
    /// </summary>
    public class TransferListResult
    {
        /// <summary>
        /// The ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
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
        /// The trf
        /// </summary>
        [JsonPropertyName("trf")]
        public string? Trf { get; set; }
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
        /// bool for if failed
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
        /// <summary>
        /// The integer
        /// </summary>
        [JsonPropertyName("@int")]
        public bool Int { get; set; }
    }

    /// <summary>
    /// The send list
    /// </summary>
    public class SendListResult
    {
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }

        /// <summary>
        /// The address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }
        /// <summary>
        /// The to address
        /// </summary>
        [JsonPropertyName("taddr")]
        public string? TAddr { get; set; }
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
        /// The trf
        /// </summary>
        [JsonPropertyName("trf")]
        public string? Trf { get; set; }
        /// <summary>
        /// The balance
        /// </summary>
        [JsonPropertyName("bal")]
        public string? Bal { get; set; }
        /// <summary>
        /// The transfer balance
        /// </summary>
        [JsonPropertyName("tbal")]
        public string? TBal { get; set; }
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
        /// bool for if failed
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
        /// <summary>
        /// The integer
        /// </summary>
        [JsonPropertyName("@int")]
        public bool Int { get; set; }
    }

    /// <summary>
    /// The account recieve list
    /// </summary>
    public class AccountRecieveListResult
    {
        /// <summary>
        /// The f address
        /// </summary>
        [JsonPropertyName("faddr")]
        public string? FAddr { get; set; }
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
        /// bool for if failed
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
        /// <summary>
        /// The integer
        /// </summary>
        [JsonPropertyName("@int")]
        public bool Int { get; set; }
    }

    public class AccumulatorListItem
    {
        /// <summary>
        /// The ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
        /// <summary>
        /// The address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }

    public class AccumulatorListJson
    {
        /// <summary>
        /// The protocol
        /// </summary>
        [JsonPropertyName("p")]
        public string? P { get; set; }
        /// <summary>
        /// The operation
        /// </summary>
        [JsonPropertyName("op")]
        public string? Op { get; set; }
        /// <summary>
        /// The accumulator list items
        /// </summary>
        [JsonPropertyName("items")]
        public List<AccumulatorListItem>? Items { get; set; }
    }

    /// <summary>
    /// The accumulator list result
    /// </summary>
    public class AccumulatorListResult
    {
        /// <summary>
        /// The opertionl
        /// </summary>
        [JsonPropertyName("op")]
        public string? Op { get; set; }
        /// <summary>
        /// The json list 
        /// </summary>
        [JsonPropertyName("json")]
        public AccumulatorListJson? Json { get; set; }
        /// <summary>
        /// The inscription id
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }
        /// <summary>
        /// The block
        /// </summary>
        [JsonPropertyName("blck")]
        public long Blck { get; set; }
        /// <summary>
        /// The transaction
        /// </summary>
        [JsonPropertyName("tx")]
        public string? Tx { get; set; }
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
    }

    /// <summary>
    /// The trade list result
    /// </summary>
    public class TradeListResult
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
        /// The ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
        /// <summary>
        /// The a tick
        /// </summary>
        [JsonPropertyName("atick")]
        public string? Atick { get; set; }
        /// <summary>
        /// The a amt
        /// </summary>
        [JsonPropertyName("aamt")]
        public string? Aamt { get; set; }
        /// <summary>
        /// The vld
        /// </summary>
        [JsonPropertyName("vld")]
        public long Vld { get; set; }
        /// <summary>
        /// The trf
        /// </summary>
        [JsonPropertyName("trf")]
        public string? Trf { get; set; }
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
        /// The num
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }
        /// <summary>
        /// The timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public long Ts { get; set; }
        /// <summary>
        /// Bool for if failed
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
    }

}
