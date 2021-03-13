using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.PaymentProcess
{
    public class PaymentProcessService : IPaymentprocessService
    {
        public async Task<IRestResponse> ProcessPayment(PaymentDetails paymentDetails)
        {
            return new RestResponse();
        }
    }
}
