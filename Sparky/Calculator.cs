using System.Numerics;

namespace Sparky
{
    public class Calculator
    {
        public List<int> numberRange = new();

        public int AddNumbers(int a,int b)
        {
            return a + b;
        }
        public double AddNumbersDouble(double a, double b)
        {
            return a + b;
        }
        public bool IsOddNumber(int a)
        {
            return a % 2 != 0;
        }
        public List<int> GetOddRange(int min, int max)
        {
            numberRange.Clear();
            for (int i = min; i < max; i++)
            {
                if (i % 2 != 0)
                {
                    numberRange.Add(i);
                }
            }
            return numberRange;
        }
    }
}

//ClassicAssert.AreEqual()
