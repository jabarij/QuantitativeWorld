#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft
{
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Newtonsoft
{
    using number = System.Double;
#endif

    internal class TimeBuilder
    {
        public number? TotalSeconds { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public number? Seconds { get; set; }
        public bool? IsNegative { get; set; }

        public bool TryBuild(out Time result)
        {
            number? totalSeconds = TotalSeconds;
            if (totalSeconds.HasValue)
            {
                result = new Time(totalSeconds.Value);
                return true;
            }

            int? hours = Hours;
            int? minutes = Minutes;
            number? seconds = Seconds;
            bool? isNegative = IsNegative;
            if (hours.HasValue
                && minutes.HasValue
                && seconds.HasValue
                && isNegative.HasValue)
            {
                result = new Time(
                    hours.Value, minutes.Value, seconds.Value, isNegative.Value);
                return true;
            }

            result = default(Time);
            return false;
        }
    }
}