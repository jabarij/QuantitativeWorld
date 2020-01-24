namespace DataStructures
{
    static class DecimalExtensions
    {
        public static decimal Pow(this decimal value, int exp)
        {
            decimal result;
            if (value == 0m || value == 1m)
                result = value;
            else if (exp > 0)
            {
                result = value;
                for (int i = 0; i < exp; i++)
                    result *= value;
            }
            else if (exp == 0)
                result = 1m;
            else
            {
                result = value;
                for (int i = 0; i < exp; i++)
                    result /= value;
            }

            return result;
        }
    }
}