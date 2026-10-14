using System;

namespace Course_Catalogue.Payments
{
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"[Online Gateway] Processing payment of ₹{amount:N2}...");
            return true;
        }
    }
}