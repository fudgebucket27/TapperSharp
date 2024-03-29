﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    /// The trade result
    /// </summary>
    public class TradeResult
    {
        /// <summary>
        /// The opertion
        /// </summary>
        [JsonPropertyName("op")]
        public string? Op { get; set; }
        /// <summary>
        /// The json list 
        /// </summary>
        [JsonPropertyName("json")]
        public TradeJson? Json { get; set; }
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
    /// The trade list json
    /// </summary>
    public class TradeJson
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
        /// The side
        /// </summary>
        [JsonPropertyName("side")]
        public string? Side { get; set; }
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
        /// The accept object
        /// </summary>
        [JsonPropertyName("accept")]
        public List<TradeAccept>? Accept { get; set; }
        /// <summary>
        /// The validness
        /// </summary>
        [JsonPropertyName("valid")]
        public string? Valid { get; set; }
    }

    /// <summary>
    /// The trade accept object
    /// </summary>
    public class TradeAccept
    {
        /// <summary>
        /// The tick
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
        /// <summary>
        /// The amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
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

    public class TradesFilledListResult
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }
        /// <summary>
        /// Secondary Address
        /// </summary>
        [JsonPropertyName("saddr")]
        public string? Saddr { get; set; }
        /// <summary>
        /// Block
        /// </summary>
        [JsonPropertyName("blck")]
        public long Blck { get; set; }
        /// <summary>
        /// Ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
        /// <summary>
        /// Secondary Ticker
        /// </summary>
        [JsonPropertyName("stick")]
        public string? Stick { get; set; }
        /// <summary>
        /// Secondary Amount
        /// </summary>
        [JsonPropertyName("samt")]
        public string? Samt { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public string? Fee { get; set; }
        /// <summary>
        /// Fee Received
        /// </summary>
        [JsonPropertyName("fee_rcv")]
        public object? FeeRcv { get; set; }  //Do not know the type at the moment, probably string though
        /// <summary>
        /// Transaction
        /// </summary>
        [JsonPropertyName("tx")]
        public string? Tx { get; set; }
        /// <summary>
        /// Inscription
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }
        /// <summary>
        /// Number
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }
        /// <summary>
        /// Secondary inscription
        /// </summary>
        [JsonPropertyName("sins")]
        public string? Sins { get; set; }
        /// <summary>
        /// Secondary Number
        /// </summary>
        [JsonPropertyName("snum")]
        public long Snum { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public long Ts { get; set; }
        /// <summary>
        /// Failure Flag
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
    }

    /// <summary>
    /// The account recieve trades filled list result
    /// </summary>
    public class AccountRecieveTradesFilledListResult
    {
        /// <summary>
        /// Secondary Address
        /// </summary>
        [JsonPropertyName("baddr")]
        public string? Baddr { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }
        /// <summary>
        /// Block
        /// </summary>
        [JsonPropertyName("blck")]
        public long Blck { get; set; }
        /// <summary>
        /// Ticker
        /// </summary>
        [JsonPropertyName("btick")]
        public string? BTick { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("bamt")]
        public string? Bamt { get; set; }
        /// <summary>
        /// Ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public string? Fee { get; set; }
        /// <summary>
        /// Fee Received
        /// </summary>
        [JsonPropertyName("fee_rcv")]
        public object? FeeRcv { get; set; }  //Do not know the type at the moment, probably string though
        /// <summary>
        /// Transaction
        /// </summary>
        [JsonPropertyName("tx")]
        public string? Tx { get; set; }

        /// <summary>
        /// Secondary inscription
        /// </summary>
        [JsonPropertyName("bins")]
        public string? Bins { get; set; }
        /// <summary>
        /// Secondary Number
        /// </summary>
        [JsonPropertyName("bnum")]
        public long Bnum { get; set; }
        /// <summary>
        /// Inscription
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }
        /// <summary>
        /// Number
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public long Ts { get; set; }
        /// <summary>
        /// Failure Flag
        /// </summary>
        [JsonPropertyName("fail")]
        public bool Fail { get; set; }
    }

    /// <summary>
    /// The token auth list result
    /// </summary>
    public class AuthListResult
    {
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }
        /// <summary>
        /// Auth
        /// </summary>
        [JsonPropertyName("auth")]
        public List<string>? Auth { get; set; }
        /// <summary>
        /// Sig
        /// </summary>
        [JsonPropertyName("sig")]
        public AuthSigListResult? Sig { get; set; }
        /// <summary>
        /// Hash
        /// </summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
        /// <summary>
        /// Salt
        /// </summary>
        [JsonPropertyName("slt")]
        public string? Slt { get; set; }
        /// <summary>
        /// Block
        /// </summary>
        [JsonPropertyName("blck")]
        public long Blck { get; set; }
        /// <summary>
        /// Inscription
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }
        /// <summary>
        /// Number
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Ts { get; set; }
    }

    /// <summary>
    /// The token auth sig list result
    /// </summary>
    public class AuthSigListResult
    {
        /// <summary>
        /// V
        /// </summary>
        [JsonPropertyName("v")]
        public string? V { get; set; }
        /// <summary>
        /// R
        /// </summary>
        [JsonPropertyName("r")]
        public string? R { get; set; }
        /// <summary>
        /// S
        /// </summary>
        [JsonPropertyName("s")]
        public string? S { get; set; }
    }

    /// <summary>
    /// The redeem list result
    /// </summary>
    public class RedeemListResult
    {
        // <summary>
        /// The address
        /// </summary>
        [JsonPropertyName("addr")]
        public string? Addr { get; set; }
        // <summary>
        /// The i address
        /// </summary>
        [JsonPropertyName("iaddr")]
        public string? Iaddr { get; set; }
        // <summary>
        /// The redeem
        /// </summary>
        [JsonPropertyName("rdm")]
        public Redeem? Rdm { get; set; }
        // <summary>
        /// The signature
        /// </summary>
        [JsonPropertyName("sig")]
        public RedeemSig? Sig { get; set; }
        // <summary>
        /// The hash
        /// </summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
        // <summary>
        /// The salt
        /// </summary>
        [JsonPropertyName("slt")]
        public string? Slt { get; set; }
        // <summary>
        /// The block
        /// </summary>
        [JsonPropertyName("blck")]
        public long Blck { get; set; }
        // <summary>
        /// The inscription
        /// </summary>
        [JsonPropertyName("ins")]
        public string? Ins { get; set; }
        // <summary>
        /// The num
        /// </summary>
        [JsonPropertyName("num")]
        public long Num { get; set; }
        // <summary>
        /// The timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public long Ts { get; set; }
    }

    /// <summary>
    /// The redeem
    /// </summary>
    public class Redeem
    {
        // <summary>
        /// The items
        /// </summary>
        [JsonPropertyName("items")]
        public List<RedeemItem>? Items { get; set; }
        // <summary>
        /// The  auth
        /// </summary>
        [JsonPropertyName("auth")]
        public string? Auth { get; set; }
        // <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }

    /// <summary>
    /// The redeem item
    /// </summary>
    public class RedeemItem
    {
        /// <summary>
        /// The ticker
        /// </summary>
        [JsonPropertyName("tick")]
        public string? Tick { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amt")]
        public string? Amt { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }
    /// <summary>
    /// The redeem signature
    /// </summary>
    public class RedeemSig
    {
        /// <summary>
        /// V
        /// </summary>
        [JsonPropertyName("v")]
        public string? V { get; set; }
        /// <summary>
        /// R
        /// </summary>
        [JsonPropertyName("r")]
        public string? R { get; set; }
        /// <summary>
        /// S
        /// </summary>
        [JsonPropertyName("s")]
        public string? S { get; set; }
    }


}
