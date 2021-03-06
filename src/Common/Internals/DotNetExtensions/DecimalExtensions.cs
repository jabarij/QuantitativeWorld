﻿namespace Common.Internals.DotNetExtensions
{
    internal static class DecimalExtensions
    {
        public static decimal Pow(this decimal value, int exp)
        {
            decimal result;
            if (exp == 0)
                result = 1m;
            else if (value == 0m || value == 1m)
                result = value;
            else if (exp > 0)
            {
                result = 1m;
                for (int i = 0; i < exp; i++)
                    result *= value;
            }
            else
            {
                result = 1m;
                for (int i = 0; i > exp; i--)
                    result *= value;
                result = 1 / result;
            }

            return result;
        }
    }
}