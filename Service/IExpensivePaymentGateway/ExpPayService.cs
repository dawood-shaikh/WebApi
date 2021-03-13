using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IExpensivePaymentGateway
{
    public class ExpPayService : IExpPayService
    {
        public async Task<IRestResponse> ExpPayProcess(PaymentDetails paymentDetails)
        {
            IRestResponse restResponse = new RestResponse();
            if (paymentDetails.Amount > 20 && paymentDetails.Amount <= 500 && paymentDetails.CreditCardNumber != null)
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
