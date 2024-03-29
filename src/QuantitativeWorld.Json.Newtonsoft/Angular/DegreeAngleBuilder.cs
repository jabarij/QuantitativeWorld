﻿#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft.Angular
{
    using QuantitativeWorld.Angular;
    using number = System.Double;
#endif

    internal class DegreeAngleBuilder
    {
        public number? TotalSeconds { get; set; }
        public int? Circles { get; set; }
        public int? Degrees { get; set; }
        public int? Minutes { get; set; }
        public number? Seconds { get; set; }
        public bool? IsNegative { get; set; }

        public bool TryBuild(out DegreeAngle result)
        {
            number? totalSeconds = TotalSeconds;
            if (totalSeconds.HasValue)
            {
                result = new DegreeAngle(totalSeconds.Value);
                return true;
            }

            int? circles = Circles;
            int? degrees = Degrees;
            int? minutes = Minutes;
            number? seconds = Seconds;
            bool? isNegative = IsNegative;
            if (circles.HasValue
                && degrees.HasValue
                && minutes.HasValue
                && seconds.HasValue
                && isNegative.HasValue)
            {
                result = new DegreeAngle(
                    circles.Value, degrees.Value, minutes.Value, seconds.Value, isNegative.Value);
                return true;
            }

            result = default(DegreeAngle);
            return false;
        }
    }
}