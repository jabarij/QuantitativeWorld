namespace QuantitativeWorld.Text.Json
{
    internal class TimeBuilder
    {
        public double? TotalSeconds { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public double? Seconds { get; set; }
        public bool? IsNegative { get; set; }

        public bool TryBuild(out Time result)
        {
            double? totalSeconds = TotalSeconds;
            if (totalSeconds.HasValue)
            {
                result = new Time(totalSeconds.Value);
                return true;
            }

            int? hours = Hours;
            int? minutes = Minutes;
            double? seconds = Seconds;
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