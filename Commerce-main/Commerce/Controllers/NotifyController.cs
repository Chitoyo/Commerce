using Commerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Commerce.Controllers
{
    public class NotifyController : ControllerBase
    {
		private ICommerce GetPayType(string option)
		{
			switch (option)
			{
				case "newbPay":
				case "newbPayPeriod":
					return new NewebpayService();
				case "ECPay":
				case "ECPayPeriod":
					return new ECPayService();

				default: throw new ArgumentException("No Such option");
			}
		}

		[HttpGet]
		public bool GetType()
		{
			return true;
		}

		/// <summary>
		/// 支付通知網址
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage CallbackNotify(string bank)
		{
			var service = GetPayType(bank);
			var result = service.GetCallbackResult(Request.Form);

			//TODO 支付成功後 可做後續訂單處理

			return ResponseOK();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public HttpResponseMessage NewebPayPeriodNotify()
		{
			var result = new NewebpayService().GetCallbackResult(Request.Form, "Period");
			return ResponseOK();
		}


		/// <summary>
		/// 回傳給 綠界 失敗
		/// </summary>
		/// <returns></returns>
		private HttpResponseMessage ResponseError()
		{
			var response = new HttpResponseMessage();
			response.Content = new StringContent("0|Error");
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
			return response;
		}

		/// <summary>
		/// 回傳給 綠界 成功
		/// </summary>
		/// <returns></returns>
		private HttpResponseMessage ResponseOK()
		{
			var response = new HttpResponseMessage();
			response.Content = new StringContent("1|OK");
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
			return response;
		}
	}
}
