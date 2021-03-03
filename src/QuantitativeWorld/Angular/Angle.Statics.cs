#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using Constants = DecimalQuantitativeWorld.DecimalConstants;
#else
namespace QuantitativeWorld.Angular
{
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct Angle
    {
        public static readonly Angle Right = new Angle(Constants.Quarter);
        public static readonly Angle Straight = new Angle(Constants.Half);
        public static readonly Angle Full = new Angle(Constants.One);
    }
}
