using System;

namespace Course_Catalogue.Services
{
    public struct FeeSummary
    {
        public decimal BaseFee;
        public decimal TotalDiscount;
        public decimal FinalFee;

        public FeeSummary(decimal baseFee, decimal totalDiscount, decimal finalFee)
        {
            BaseFee = baseFee;
            TotalDiscount = totalDiscount;
            FinalFee = finalFee;
        }
    }

    public class FeeCalculator
    {
        public (decimal baseFee, decimal totalDiscount, decimal finalFee) CalculateFee(decimal baseFee, decimal surcharge)
        {
            if (baseFee < 0 || surcharge < 0)
            {
                throw new ArgumentException("Fee and surcharge cannot be negative.");
            }



            decimal totalDiscount = 0m;
            decimal finalFee = baseFee + surcharge;
            return (baseFee, totalDiscount, finalFee);
        }

        public (decimal baseFee, decimal totalDiscount, decimal finalFee) CalculateFee(decimal baseFee, bool isEarlyBird, bool isGroupDiscount, decimal surcharge)
        {
            if (baseFee < 0 || surcharge < 0)
            {
                throw new ArgumentException("Fee and surcharge cannot be negative.");
            }

            decimal earlyBirdDiscount = isEarlyBird ? baseFee * 0.10m : 0m;
            decimal groupDiscount = isGroupDiscount ? baseFee * 0.05m : 0m;
            decimal totalDiscount = earlyBirdDiscount + groupDiscount;

            decimal discountedBase = baseFee - totalDiscount;
            if (discountedBase < 0)
            {
                discountedBase = 0m;
            }

            decimal finalFee = discountedBase + surcharge;
            return (baseFee, totalDiscount, finalFee);
        }

        public void UpdateEnrollmentCounter(ref int activeCount)
        {
            activeCount++;
        }

        public bool TryParseLearnerId(string input, out string validatedId)
        {
            validatedId = null;
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            validatedId = input.Trim().ToUpper();
            return true;
        }

        public void DisplayFeeBreakdown(in FeeSummary summary)
        {
            Console.WriteLine("\n--- Fee Breakdown ---");
            Console.WriteLine($"Base Fee      : ₹{summary.BaseFee:N2}");
            Console.WriteLine($"Total Discount: ₹{summary.TotalDiscount:N2}");
            Console.WriteLine($"Final Payable : ₹{summary.FinalFee:N2}");
            Console.WriteLine("---------------------\n");
        }
    }
}