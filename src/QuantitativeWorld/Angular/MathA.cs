using System;

namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
    using Math = QuantitativeWorld.MathDecimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public static class MathA
    {
        #region RadianAngle

        public static number Cos(RadianAngle a) =>
            Math.Cos(a.Radians);
        public static number Sin(RadianAngle a) =>
            Math.Sin(a.Radians);
        public static number Tan(RadianAngle a) =>
            Math.Tan(a.Radians);

        public static RadianAngle Acos(number x) =>
            new RadianAngle(Math.Acos(x));
        public static RadianAngle Asin(number x) =>
            new RadianAngle(Math.Asin(x));
        public static RadianAngle Atan(number x) =>
            new RadianAngle(Math.Atan(x));
        public static RadianAngle Atan2(number y, number x) =>
            new RadianAngle(Math.Atan2(y, x));

        #endregion

        #region DegreeAngle

        public static number Cos(DegreeAngle a) =>
            Cos(a.ToRadianAngle());
        public static number Sin(DegreeAngle a) =>
            Sin(a.ToRadianAngle());
        public static number Tan(DegreeAngle a) =>
            Tan(a.ToRadianAngle());

        #endregion

        #region Angle

        public static number Cos(Angle a) =>
            Cos(RadianAngle.FromAngle(a));
        public static number Sin(Angle a) =>
            Sin(RadianAngle.FromAngle(a));
        public static number Tan(Angle a) =>
            Tan(RadianAngle.FromAngle(a));

        #endregion
    }
}
