namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct DegreeAngle
    {
        public static readonly DegreeAngle Right = new DegreeAngle(Constants.ArcsecondsPerTurn * Constants.Quarter);
        public static readonly DegreeAngle Straight = new DegreeAngle(Constants.ArcsecondsPerTurn * Constants.Half);
        public static readonly DegreeAngle Full = new DegreeAngle(Constants.ArcsecondsPerTurn);
    }
}
