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

        public static double Cosh(RadianAngle a) =>
            Math.Cosh(a.Radians);
        public static double Sinh(RadianAngle a) =>
            Math.Sinh(a.Radians);
        public static double Tanh(RadianAngle a) =>
            Math.Tanh(a.Radians);

        public static double Acos(RadianAngle a) =>
            Math.Acos(a.Radians);
        public static double Asin(RadianAngle a) =>
            Math.Asin(a.Radians);
        public static double Atan(RadianAngle a) =>
            Math.Atan(a.Radians);

        #endregion

        #region DegreeAngle

        public static double Cos(DegreeAngle a) =>
            Cos(a.ToRadianAngle());
        public static double Sin(DegreeAngle a) =>
            Sin(a.ToRadianAngle());
        public static double Tan(DegreeAngle a) =>
            Tan(a.ToRadianAngle());

        public static double Cosh(DegreeAngle a) =>
            Cosh(a.ToRadianAngle());
        public static double Sinh(DegreeAngle a) =>
            Sinh(a.ToRadianAngle());
        public static double Tanh(DegreeAngle a) =>
            Tanh(a.ToRadianAngle());

        public static double Acos(DegreeAngle a) =>
            Acos(a.ToRadianAngle());
        public static double Asin(DegreeAngle a) =>
            Asin(a.ToRadianAngle());
        public static double Atan(DegreeAngle a) =>
            Atan(a.ToRadianAngle());

        #endregion

        #region Angle

        public static double Cos(Angle a) =>
            Cos(RadianAngle.FromAngle(a));
        public static double Sin(Angle a) =>
            Sin(RadianAngle.FromAngle(a));
        public static double Tan(Angle a) =>
            Tan(RadianAngle.FromAngle(a));

        public static double Cosh(Angle a) =>
            Cosh(RadianAngle.FromAngle(a));
        public static double Sinh(Angle a) =>
            Sinh(RadianAngle.FromAngle(a));
        public static double Tanh(Angle a) =>
            Tanh(RadianAngle.FromAngle(a));

        public static double Acos(Angle a) =>
            Acos(RadianAngle.FromAngle(a));
        public static double Asin(Angle a) =>
            Asin(RadianAngle.FromAngle(a));
        public static double Atan(Angle a) =>
            Atan(RadianAngle.FromAngle(a));

        #endregion
    }
}
