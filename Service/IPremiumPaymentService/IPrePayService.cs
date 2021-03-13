using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IPremiumPaymentService
{
    public interface IPrePayService
    {
        Task<IRestResponse> PrePayProcess(PaymentDetails paymentDetails);
    }
}
