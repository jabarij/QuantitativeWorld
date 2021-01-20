using System;

namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct Angle
    {
        public static readonly Angle Right = new Angle(Constants.Quarter);
        public static readonly Angle Straight = new Angle(Constants.Half);
        public static readonly Angle Full = new Angle(Constants.One);
    }
}
