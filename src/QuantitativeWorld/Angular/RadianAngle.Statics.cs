#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using Constants = DecimalConstants;
#else
namespace QuantitativeWorld.Angular
{
    using Constants = DoubleConstants;
#endif

    partial struct RadianAngle
    {
        public static readonly RadianAngle PI = new RadianAngle(Constants.PI);
        public static readonly RadianAngle Right = PI * Constants.Half;
        public static readonly RadianAngle Straight = PI;
        public static readonly RadianAngle Full = PI * 2;
    }
}
