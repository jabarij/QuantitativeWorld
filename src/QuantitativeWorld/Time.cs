using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    public partial struct Time
    {
        public const int MinHours = 0;
        public const int MaxHours = 359;
        public const int MinMinutes = 0;
        public const int MaxMinutes = 59;
        public const double MinSeconds = 0d;
        public const double ExclusiveMaxSeconds = 60d;

        public static readonly Time Zero = new Time(0d);

        private const double EmptyValue = 0d;

        private static readonly ValueRange<double> _secondsRange =
            new ValueRange<double>(MinSeconds, ExclusiveMaxSeconds, IntervalBoundaryType.Closed, IntervalBoundaryType.Open);

        private readonly double? _totalSeconds;

        public Time(double totalSeconds)
        {
            Assert.IsNotNaN(totalSeconds, nameof(totalSeconds));
            _totalSeconds = totalSeconds;
        }
        public Time(int hours, int minutes, double seconds, bool isNegative)
        {
            Assert.IsGreaterThanOrEqual(hours, 0, nameof(hours));
            Assert.IsInRange(minutes, MinMinutes, MaxMinutes, nameof(minutes));
            Assert.IsInRange(seconds, _secondsRange, nameof(seconds));
            double totalSeconds = (hours * Constants.SecondsPerHour + minutes * Constants.SecondsPerMinute + seconds);
            if (isNegative)
                totalSeconds *= -1;
            _totalSeconds = totalSeconds;
        }
        public Time(int hours, int minutes, double seconds)
            : this(Math.Abs(hours), minutes, seconds, hours < 0) { }
        public Time(int minutes, double seconds)
            : this(0, Math.Abs(minutes), seconds, minutes < 0) { }

        public double TotalSeconds => _totalSeconds ?? EmptyValue;
        public double TotalMinutes =>
            TotalSeconds / Constants.SecondsPerMinute;
        public double TotalHours =>
            TotalSeconds / Constants.SecondsPerHour;
        public bool IsNegative =>
            TotalSeconds < 0d;

        public int Hours =>
            Math.Abs((int)(TotalMinutes / Constants.MinutesPerHour));
        public int Minutes =>
            Math.Abs((int)(TotalMinutes % Constants.MinutesPerHour));
        public double Seconds =>
            Math.Abs(TotalSeconds % Constants.SecondsPerMinute);

        public TimeSpan ToTimeSpan() =>
            TimeSpan.FromSeconds(TotalSeconds);

        public bool IsZero() =>
            TotalSeconds.Equals(EmptyValue);

        public override string ToString() =>
            ToTimeSpan().ToString();

        public static Time FromTimeSpan(TimeSpan timeSpan) =>
            new Time(timeSpan.TotalSeconds);
    }
}