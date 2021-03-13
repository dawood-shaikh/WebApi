using Model;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Service.PaymentProcess
{
    public interface IPaymentprocessService
    {
        Task<IRestResponse> ProcessPayment(PaymentDetails paymentDetails);
    }
}
