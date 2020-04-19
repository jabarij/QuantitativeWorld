using QuantitativeWorld.Angular;

namespace QuantitativeWorld.Text.Json.Angular
{
    internal class DegreeAngleBuilder
    {
        public double? TotalSeconds { get; set; }
        public int? Circles { get; set; }
        public int? Degrees { get; set; }
        public int? Minutes { get; set; }
        public double? Seconds { get; set; }
        public bool? IsNegative { get; set; }

        public bool TryBuild(out DegreeAngle result)
        {
            double? totalSeconds = TotalSeconds;
            if (totalSeconds.HasValue)
            {
                result = new DegreeAngle(totalSeconds.Value);
                return true;
            }

            int? circles = Circles;
            int? degrees = Degrees;
            int? minutes = Minutes;
            double? seconds = Seconds;
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