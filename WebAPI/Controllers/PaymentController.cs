using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using RestSharp;
using Service.ICheapPaymentGateway;
using Service.IExpensivePaymentGateway;
using Service.IPremiumPaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;
using WebAPI.Repositories;
using PaymentDetails = Model.PaymentDetails;

namespace WebAPI.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IRepository<PaymentStatustbl> _Repository;
        private readonly ICheapPayService _cheapPayService;
        private readonly IExpPayService _expPayService;
        private readonly IPrePayService _prePayService;
        public PaymentController(IRepository<PaymentStatustbl> dataRepository, ICheapPayService cheapPayService, IExpPayService expPayService, IPrePayService prePayService)
        {
            _Repository = dataRepository;
            _cheapPayService = cheapPayService;
            _expPayService = expPayService;
            _prePayService = prePayService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentDetails paymentDetails)
        {
            try
            {
                IRestResponse response = null;
                if (ModelState.IsValid)
                {
                    if (paymentDetails == null)
                    {
                        return BadRequest("Employee is null.");
                    }

                    if (paymentDetails.Amount <= 20)
                    {
                        response = await _cheapPayService.CheapPayProcess(paymentDetails); // Cheap Payment Service Call 
                    }
                    else if (paymentDetails.Amount > 20 && paymentDetails.Amount <= 500)
                    {
                        response = await _expPayService.ExpPayProcess(paymentDetails); // Expensive Payment Service Call 
                    }
                    else if (paymentDetails.Amount > 500)
                    {
                        response = await _prePayService.PrePayProcess(paymentDetails); // Premium Payment Service Call 
                    }

                    // Add status of the payment in Database
                    var paymentStatustbl = new PaymentStatustbl();
                    paymentStatustbl.CreditCardNumber = paymentDetails.CreditCardNumber;
                    paymentStatustbl.Status = response.Content;
                    _Repository.Add(paymentStatustbl);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        return Ok("Payment processed Successfully !!");
                    else
                        return BadRequest("Payment processed Failed !!");

                }
                else
                {
                    return BadRequest(ModelState.Values.ToString());
                }
            }
            catch(Exception ex)
            {
                 return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}
