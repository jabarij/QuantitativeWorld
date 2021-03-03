using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld
{
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld
{
    using Constants = DoubleConstants;
    using number = Double;
#endif

    public partial struct Time
    {
        private const number MinTotalSeconds = number.MinValue;
        private const number MaxTotalSeconds = number.MaxValue;

        public const int MinHours = 0;
        public const int MaxHours = 359;
        public const int MinMinutes = 0;
        public const int MaxMinutes = 59;
        public const number MinSeconds = Constants.Zero;
        public const number ExclusiveMaxSeconds = Constants.SecondsPerMinute;

        public static readonly Time Zero = new Time(Constants.Zero);

        private const number EmptyValue = Constants.Zero;

        private static readonly ValueRange<number> _secondsRange =
            new ValueRange<number>(MinSeconds, ExclusiveMaxSeconds, IntervalBoundaryType.Closed, IntervalBoundaryType.Open);

        private readonly number? _totalSeconds;

        public Time(number totalSeconds)
        {
            Assert.IsNotNaN(totalSeconds, nameof(totalSeconds));
            Assert.IsInRange(totalSeconds, MinTotalSeconds, MaxTotalSeconds, nameof(totalSeconds));
            _totalSeconds = totalSeconds;
        }
        public Time(int hours, int minutes, number seconds, bool isNegative)
        {
            Assert.IsGreaterThanOrEqual(hours, 0, nameof(hours));
            Assert.IsInRange(minutes, MinMinutes, MaxMinutes, nameof(minutes));
            Assert.IsInRange(seconds, _secondsRange, nameof(seconds));
            number totalSeconds = (hours * Constants.SecondsPerHour + minutes * Constants.SecondsPerMinute + seconds);
            if (isNegative)
                totalSeconds *= -1;
            _totalSeconds = totalSeconds;
        }
        public Time(int hours, int minutes, number seconds)
            : this(Math.Abs(hours), minutes, seconds, hours < 0) { }
        public Time(int minutes, number seconds)
            : this(0, Math.Abs(minutes), seconds, minutes < 0) { }
        private Time(number? totalSeconds)
        {
            _totalSeconds = totalSeconds;
        }

        public number TotalSeconds => _totalSeconds ?? EmptyValue;
        public number TotalMinutes =>
            TotalSeconds / Constants.SecondsPerMinute;
        public number TotalHours =>
            TotalSeconds / Constants.SecondsPerHour;
        public bool IsNegative =>
            TotalSeconds < Constants.Zero;

        public int Hours =>
            Math.Abs((int)(TotalMinutes / Constants.MinutesPerHour));
        public int Minutes =>
            Math.Abs((int)(TotalMinutes % Constants.MinutesPerHour));
        public number Seconds =>
            Math.Abs(TotalSeconds % Constants.SecondsPerMinute);

        public TimeSpan ToTimeSpan() =>
            TimeSpan.FromSeconds((double)TotalSeconds);

        public bool IsZero() =>
            TotalSeconds.Equals(EmptyValue);

        public override string ToString() =>
            ToTimeSpan().ToString();

        public static Time FromTimeSpan(TimeSpan timeSpan) =>
            new Time((number)timeSpan.TotalSeconds);
    }
}