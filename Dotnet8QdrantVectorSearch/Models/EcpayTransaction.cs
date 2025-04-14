using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class EcpayTransaction
{
    public string GroupOrderId { get; set; }

    /// <summary>
    /// 特店訂單編號(我們所定義的，必須唯一)
    /// </summary>
    public string MerchantTradeNo { get; set; }

    /// <summary>
    /// 綠界的交易編號
    /// </summary>
    public string TradeNo { get; set; }

    /// <summary>
    /// 訂單成立時間
    /// </summary>
    public DateTime? TradeDate { get; set; }

    /// <summary>
    /// 交易金額
    /// </summary>
    public int? TradeAmt { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    public string PaymentType { get; set; }

    public decimal? PaymentTypeChargeFee { get; set; }

    /// <summary>
    /// 付款時間
    /// </summary>
    public DateTime? PaymentDate { get; set; }

    /// <summary>
    /// 交易狀態
    /// </summary>
    public int? RtnCode { get; set; }

    /// <summary>
    /// 交易訊息
    /// </summary>
    public string RtnMsg { get; set; }
}
