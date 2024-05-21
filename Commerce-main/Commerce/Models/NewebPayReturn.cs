using System;

namespace Commerce.Models
{
    public class NewebPayReturn<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class NewebPayQueryResult
    {
        public string MerchantID { get; set; }
        public string MerchantOrderNo { get; set; }
        public string TradeNo { get; set; }
        public int Amt { get; set; }
        public string TradeStatus { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime PayTime { get; set; }
        public string CheckCode { get; set; }
        public DateTime FundTime { get; set; }
        public string ShopMerchantID { get; set; }

    }

    public class NewebPayPeriodResult
    {
        public string MerchantID { get; set; }
        public string MerchantOrderNo { get; set; }
        public string PeriodType { get; set; }
        public int AuthTimes { get; set; }
        public string AuthTime { get; set; }
        public string DateArray { get; set; }
        public string TradeNo { get; set; }
        public string CardNo { get; set; }
        public decimal PeriodAmt { get; set; }
        public string AuthCode { get; set; }
        public string RespondCode { get; set; }
        public string EscrowBank { get; set; }
        public string AuthBank { get; set; }
        public string PaymentMethod { get; set; }
        public string PeriodNo { get; set; }
        public string Extday { get; set; }

    }
}
