using Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IExpensivePaymentGateway
{
    public interface IExpPayService
    {
        Task<IRestResponse> ExpPayProcess(PaymentDetails paymentDetails);
    }
}
