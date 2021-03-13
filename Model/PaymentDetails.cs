using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class PaymentDetails
    {
        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public int? SecurityCode { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be positive")]
        public decimal Amount { get; set; }
    }
}
