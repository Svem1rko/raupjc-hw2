using System;
using System.Threading.Tasks;

namespace zadatak6
{
    public class Class1
    {
        public static async Task<int> FactorialDigitSum(int n)
        {
            int fact = 1;
            int sum = 0;

            for (int i = 2; i <= n; i++)
            {
                fact *= i;
            }

            while (fact > 0)
            {
                sum += fact % 10;
                fact /= 10;
            }

            return sum;
        }

        public static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThisAsync(10) + await IKnowWhoKnowsThisAsync(5);
        }
        private static async Task<int> IKnowWhoKnowsThisAsync(int n)
        {
            return await FactorialDigitSum(n);
        }
    }
}