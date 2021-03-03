#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using Constants = DecimalConstants;
#else
namespace QuantitativeWorld.Angular
{
    using Constants = DoubleConstants;
#endif

    partial struct DegreeAngle
    {
        public static readonly DegreeAngle Right = new DegreeAngle(Constants.ArcsecondsPerTurn * Constants.Quarter);
        public static readonly DegreeAngle Straight = new DegreeAngle(Constants.ArcsecondsPerTurn * Constants.Half);
        public static readonly DegreeAngle Full = new DegreeAngle(Constants.ArcsecondsPerTurn);
    }
}
