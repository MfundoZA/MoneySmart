using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySmart.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Transaction()
        {

        }

        public Transaction(string description, Type type, decimal amount, PaymentMethod paymentMethod)
        {
            Description = description;
            Type = type;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }

        public Transaction(int id, string description, Type type, decimal amount, PaymentMethod paymentMethod)
        {
            Id = id;
            Description = description;
            Type = type;
            Amount = amount;
            PaymentMethod = paymentMethod;
        }
    }
}
