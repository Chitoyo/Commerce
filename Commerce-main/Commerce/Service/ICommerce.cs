using Commerce.Models;
using Microsoft.AspNetCore.Http;

namespace Commerce.Service
{
    public interface ICommerce
    {
        string GetCallBack(SendToNewebPayIn inModel);
        string GetPeriodCallBack(SendToNewebPayIn inModel);        
        Result GetCallbackResult(IFormCollection form);
    }
}
