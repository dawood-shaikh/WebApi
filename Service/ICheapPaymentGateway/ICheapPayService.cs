using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;
using RestSharp;

namespace Service.ICheapPaymentGateway
{
    public interface ICheapPayService
    {
        Task<IRestResponse> CheapPayProcess(PaymentDetails paymentDetails);
    }
}
