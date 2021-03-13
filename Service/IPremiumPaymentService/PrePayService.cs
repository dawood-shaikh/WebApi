using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IPremiumPaymentService
{
    public class PrePayService : IPrePayService
    {
        public async Task<IRestResponse> PrePayProcess(PaymentDetails paymentDetails)
        {
            IRestResponse restResponse = new RestResponse();
            if (paymentDetails.Amount >= 501 && paymentDetails.CreditCardNumber != null)
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
