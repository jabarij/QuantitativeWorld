#if DECIMAL
namespace DecimalCommon.Internals.DotNetExtensions
#else
namespace Common.Internals.DotNetExtensions
#endif
{
    internal static class DoubleExtensions
    {
        public static double Pow(this double value, int exp)
        {
            double result;
            if (exp == 0)
                result = 1d;
            else if (value == 0d || value == 1d)
                result = value;
            else if (exp > 0)
            {
                result = 1d;
                for (int i = 0; i < exp; i++)
                    result *= value;
            }
            else
            {
                result = 1d;
                for (int i = 0; i > exp; i--)
                    result *= value;
                result = 1 / result;
            }

            return result;
        }
    }
}