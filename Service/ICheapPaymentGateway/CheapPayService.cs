using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ICheapPaymentGateway
{
    public class CheapPayService : ICheapPayService
    {
        public async Task<IRestResponse> CheapPayProcess(PaymentDetails paymentDetails)
        {
            IRestResponse restResponse = new RestResponse();
            if (paymentDetails.Amount <= 20 && paymentDetails.CreditCardNumber != null)
            {
                restResponse.StatusCode = System.Net.HttpStatusCode.OK;
                restResponse.ResponseStatus = ResponseStatus.Completed;
                restResponse.Content = "Success";
            }
            else
            {
                restResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                restResponse.ResponseStatus = ResponseStatus.Aborted;
                restResponse.Content = "Failed";
            }
            return restResponse;
        }
    }
}
