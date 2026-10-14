using System;

namespace Course_Catalogue.Utilities
{
    public static class InputValidator
    {
        public static int GetValidInteger(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    return result;
                }
                Console.WriteLine("-> Error: Please enter a valid integer.");
            }
        }

        public static decimal GetValidDecimal(string prompt)
        {
            decimal result;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("-> Error: Please enter a valid positive amount.");
            }
        }
    }
}