using System;

namespace QuantitativeWorld
{
    static class MathDecimal
    {
        public static decimal Sqrt(decimal d) =>
            (decimal)Math.Sqrt((double)d);

        public static decimal Sin(decimal d) =>
            (decimal)Math.Sin((double)d);

        public static decimal Cos(decimal d) =>
            (decimal)Math.Cos((double)d);

        public static decimal Tan(decimal d) =>
            (decimal)Math.Tan((double)d);

        public static decimal Asin(decimal d) =>
            (decimal)Math.Asin((double)d);

        public static decimal Acos(decimal d) =>
            (decimal)Math.Acos((double)d);

        public static decimal Atan(decimal a) =>
            (decimal)Math.Atan((double)a);

        public static decimal Atan2(decimal y, decimal x) =>
            (decimal)Math.Atan2((double)y, (double)x);
    }
}