using QuantitativeWorld.Parsing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    partial struct SpeedUnit
    {
        [Predefined]
        public static readonly SpeedUnit MetrePerSecond = new SpeedUnit(name: "metre per second", abbreviation: "m/s", valueInMetresPerSecond: Constants.One);
        [Predefined]
        public static readonly SpeedUnit KilometrePerHour = new SpeedUnit(name: "kilometre per hour", abbreviation: "km/h", valueInMetresPerSecond: Constants.MetresPerSecondPerKilometrePerHour);

        [Predefined]
        public static readonly SpeedUnit FootPerSecond = new SpeedUnit(name: "foot per second", abbreviation: "ft/s", valueInMetresPerSecond: Constants.MetresPerSecondPerFootPerSecond);
        [Predefined]
        public static readonly SpeedUnit MilePerHour = new SpeedUnit(name: "mile per hour", abbreviation: "mph", valueInMetresPerSecond: Constants.MetresPerSecondPerMilePerHour);

        public static IEnumerable<SpeedUnit> GetPredefinedUnits() =>
            typeof(SpeedUnit).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(e => e.IsDefined(typeof(PredefinedAttribute), PredefinedAttribute.Inherited) && e.FieldType == typeof(SpeedUnit))
            .Select(e => (SpeedUnit)e.GetValue(null));
    }
}
