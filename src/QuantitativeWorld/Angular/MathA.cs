using System;

namespace QuantitativeWorld.Angular
{
    public static class MathA
    {
        #region RadianAngle

        public static double Cos(RadianAngle a) =>
            Math.Cos(a.Radians);
        public static double Sin(RadianAngle a) =>
            Math.Sin(a.Radians);
        public static double Tan(RadianAngle a) =>
            Math.Tan(a.Radians);

        public static RadianAngle Acos(double x) =>
            new RadianAngle(Math.Acos(x));
        public static RadianAngle Asin(double x) =>
            new RadianAngle(Math.Asin(x));
        public static RadianAngle Atan(double x) =>
            new RadianAngle(Math.Atan(x));
        public static RadianAngle Atan2(double y, double x) =>
            new RadianAngle(Math.Atan2(y, x));

        #endregion

        #region DegreeAngle

        public static double Cos(DegreeAngle a) =>
            Cos(a.ToRadianAngle());
        public static double Sin(DegreeAngle a) =>
            Sin(a.ToRadianAngle());
        public static double Tan(DegreeAngle a) =>
            Tan(a.ToRadianAngle());

        #endregion

        #region Angle

        public static double Cos(Angle a) =>
            Cos(RadianAngle.FromAngle(a));
        public static double Sin(Angle a) =>
            Sin(RadianAngle.FromAngle(a));
        public static double Tan(Angle a) =>
            Tan(RadianAngle.FromAngle(a));

        #endregion
    }
}
