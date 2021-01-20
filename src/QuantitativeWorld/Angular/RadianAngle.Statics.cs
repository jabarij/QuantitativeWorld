namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct RadianAngle
    {
        public static readonly RadianAngle PI = new RadianAngle(Constants.PI);
        public static readonly RadianAngle Right = PI * Constants.Half;
        public static readonly RadianAngle Straight = PI;
        public static readonly RadianAngle Full = PI * 2;
    }
}
