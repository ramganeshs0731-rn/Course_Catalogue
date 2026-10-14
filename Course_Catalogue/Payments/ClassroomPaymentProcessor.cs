using System;

namespace Course_Catalogue.Payments
{
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(decimal amount)
        {
            Console.WriteLine($"[Classroom Counter] Processing cash/card payment of ₹{amount:N2}...");
            return true;
        }
    }
}