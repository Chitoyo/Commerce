namespace Commerce.Models
{
	public class SendToNewebPayOut
	{
		public string MerchantID { get; set; }
		public string Version { get; set; }
		public string TradeInfo { get; set; }
		public string TradeSha { get; set; }
	}

	public class SendToNewebPayPeriod
	{
		public string MerchantID_ { get; set; }
		public string PostData_ { get; set; }
	}
}
