using Commerce.Models;
using Commerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Commerce.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration Config;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
		}

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

		private string GetReturnValue(ICommerce service, SendToNewebPayIn inModel)
		{
			switch (inModel.PayOption)
			{
				case "newbPay":
					return service.GetCallBack(inModel);
				case "newbPayPeriod":
					return service.GetPeriodCallBack(inModel);
				case "ECPay":
					return service.GetCallBack(inModel);
				case "ECPayPeriod":
					return service.GetPeriodCallBack(inModel);

				default: throw new ArgumentException("No Such option");
			}
		}

		public IActionResult Index()
		{
			ViewData["MerchantOrderNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");  //訂單編號
			ViewData["ExpireDate"] = DateTime.Now.AddDays(3).ToString("yyyyMMdd"); //繳費有效期限       
			return View();
		}

		public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
		{
			var service = GetPayType(inModel.PayOption);

			return Json(GetReturnValue(service, inModel));
		}

		[HttpPost]
		public async Task<IActionResult> GetReturn(SendToNewebPayIn inModel)
		{
			var obj = await  new ECPayService().GetQueryCallBack(inModel.MerchantOrderNo, inModel.Amt);
			return Json(obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<string> UpdatePeriodCallBack(string orderNo, string PeriodN)
		{
			var result = await new NewebpayService().GetUpdatePeriodCallBackAsync(orderNo, PeriodN);

			return result;
		}

		/// <summary>
		/// 支付完成返回網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackReturn(string option)
		{
			var service = GetPayType(option);
			var result = service.GetCallbackResult(Request.Form);
			ViewData["ReceiveObj"] = result.ReceiveObj;
			ViewData["TradeInfo"] = result.TradeInfo;

			return View();
		}


		/// <summary>
		/// 商店取號網址
		/// </summary>
		/// <returns></returns>
		public IActionResult CallbackCustomer(string option)
		{
			var service = GetPayType(option);
			var result = service.GetCallbackResult(Request.Form);
			ViewData["ReceiveObj"] = result.ReceiveObj;
			ViewData["TradeInfo"] = result.TradeInfo;
			return View();
		}

	

	}
}