using System;
using System.Collections.Generic;
using Course_Catalogue.Models;
using Course_Catalogue.Payments;

namespace Course_Catalogue.Services
{
    public class PaymentService
    {
        private List<Payment> _payments;
        private List<Enrollment> _enrollments;
        private int _nextPaymentId = 1001;

        public PaymentService(List<Payment> payments, List<Enrollment> enrollments)
        {
            _payments = payments;
            _enrollments = enrollments;
        }

        public void ProcessPayment(int enrollmentId, bool isOnline, IPaymentProcessor processor)
        {
            Enrollment targetEnrollment = null;
            foreach (var en in _enrollments)
            {
                if (en.EnrollmentId == enrollmentId)
                {
                    targetEnrollment = en;
                    break;
                }
            }

            if (targetEnrollment == null)
            {
                Console.WriteLine("-> Error: Enrollment record not found.");
                return;
            }

            if (targetEnrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("-> Error: Cannot process payment for a cancelled enrollment.");
                return;
            }

            bool paymentSuccess = processor.ProcessPayment(targetEnrollment.FinalFee);
            PaymentStatus status = paymentSuccess ? PaymentStatus.Successful : PaymentStatus.Failed;

            Payment payment = new Payment(
                _nextPaymentId++,
                enrollmentId,
                targetEnrollment.FinalFee,
                DateTime.Now,
                status
            );

            _payments.Add(payment);

            if (paymentSuccess)
            {
                Console.WriteLine($"-> Success: Payment recorded successfully for Enrollment ID {enrollmentId}. Status: Successful.");
            }
            else
            {
                Console.WriteLine($"-> Failure: Payment failed for Enrollment ID {enrollmentId}. Status: Failed.");
            }
        }

        public void DisplayPaymentHistory()
        {
            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"| {"Payment ID",-12} | {"Enrollment ID",-14} | {"Amount",-12} | {"Date",-12} | {"Status",-10} |");
            Console.WriteLine(new string('-', 75));

            foreach (var p in _payments)
            {
                Console.WriteLine($"| {p.PaymentId,-12} | {p.EnrollmentId,-14} | ₹{p.Amount,-10:N0} | {p.PaymentDate:yyyy-MM-dd} | {p.Status,-10} |");
            }
            Console.WriteLine(new string('-', 75));
        }
    }
}