using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySmart.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Descrption { get; set; }
        public Type Type { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
