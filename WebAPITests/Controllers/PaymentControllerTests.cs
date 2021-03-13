using WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using Moq;
using WebAPI.Repositories;
using WebAPI.Model;
using Service.ICheapPaymentGateway;
using Service.IExpensivePaymentGateway;
using Service.IPremiumPaymentService;
using Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Tests
{
    public class PaymentControllerTests
    {
        public MockRepository mockRepository { get; set; }
        public Mock<IRepository<PaymentStatustbl>> mockRepo { get; set; }

        public PaymentControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);
            this.mockRepo = this.mockRepository.Create<IRepository<PaymentStatustbl>>();
           
        }
        [Fact]
        public async Task Payment_Process_WithOKReturn()
        {
            #region Arrange
            var mockCheap = new Mock<CheapPayService>();
            var mockExp = new Mock<ExpPayService>();
            var mockPre = new Mock<PrePayService>();
            var controller = new PaymentController(mockRepo.Object, mockCheap.Object, mockExp.Object, mockPre.Object);
            var paymentDetails = new PaymentDetails()
            {
                CreditCardNumber = "123456897",
                CardHolder = "Test",
                ExpirationDate = new DateTime(2021, 01, 01),
                Amount = 10
            };
            #endregion

            // Act
            var result = await controller.Post(paymentDetails);

            // Assert
            var OkResult = result as OkObjectResult;
            Assert.Equal("Payment processed Successfully !!", OkResult.Value);
        }
        [Fact]
        public async Task Payment_Process_WithBadRequestReturn()
        {
            #region Arrange
            var mockCheap = new Mock<CheapPayService>();
            var mockExp = new Mock<ExpPayService>();
            var mockPre = new Mock<PrePayService>();
            var controller = new PaymentController(mockRepo.Object, mockCheap.Object, mockExp.Object, mockPre.Object);
            var paymentDetails = new PaymentDetails()
            {
                CreditCardNumber = null,
                CardHolder = null,
                ExpirationDate = new DateTime(2021, 01, 01),
                Amount = 10
            };
            #endregion

            // Act
            var result = await controller.Post(paymentDetails);

            // Assert
            var OkResult = result as BadRequestObjectResult;
            Assert.Equal("Payment processed Failed !!", OkResult.Value);
        }
    }
}