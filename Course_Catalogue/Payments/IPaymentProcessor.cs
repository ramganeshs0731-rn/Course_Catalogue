namespace Course_Catalogue.Payments
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }
}