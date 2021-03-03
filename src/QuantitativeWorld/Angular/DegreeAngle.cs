using Common.Internals.DotNetExtensions;
using System;

#if DECIMAL
namespace DecimalQuantitativeWorld.Angular
{
    using DecimalQuantitativeWorld.Interfaces;
    using Constants = DecimalConstants;
    using number = Decimal;
#else
namespace QuantitativeWorld.Angular
{
    using QuantitativeWorld.Interfaces;
    using Constants = DoubleConstants;
    using number = Double;
#endif

    public partial struct DegreeAngle : ILinearQuantity<AngleUnit>
    {
        private const number MinTotalSeconds = number.MinValue;
        private const number MaxTotalSeconds = number.MaxValue;

        public const int MinDegrees = 0;
        public const int MaxDegrees = 359;
        public const int MinMinutes = 0;
        public const int MaxMinutes = 59;
        public const number MinSeconds = Constants.Zero;
        public const number ExclusiveMaxSeconds = Constants.SecondsPerMinute;

        public static readonly DegreeAngle Zero = new DegreeAngle(Constants.Zero);

        private static readonly ValueRange<number> _secondsRange =
            new ValueRange<number>(MinSeconds, ExclusiveMaxSeconds, IntervalBoundaryType.Closed, IntervalBoundaryType.Open);

        private int? _circles;
        private int? _degrees;
        private int? _minutes;
        private number? _seconds;

        public DegreeAngle(number totalSeconds)
            : this(totalSeconds, true) { }
        public DegreeAngle(int circles, int degrees, int minutes, number seconds, bool isNegative)
        {
            Assert.IsGreaterThanOrEqual(circles, 0, nameof(circles));
            Assert.IsInRange(degrees, MinDegrees, MaxDegrees, nameof(degrees));
            Assert.IsInRange(minutes, MinMinutes, MaxMinutes, nameof(minutes));
            Assert.IsInRange(seconds, _secondsRange, nameof(seconds));
            number totalSeconds = (circles * Constants.ArcsecondsPerTurn + degrees * Constants.ArcsecondsPerDegree + minutes * Constants.ArcsecondsPerArcminute + seconds);
            if (isNegative)
                totalSeconds *= -1;
            Assert.IsInRange(totalSeconds, MinTotalSeconds, MaxTotalSeconds, nameof(totalSeconds));
            TotalSeconds = totalSeconds;
            _circles = circles;
            _degrees = degrees;
            _minutes = minutes;
            _seconds = seconds;
        }
        public DegreeAngle(int circles, int degrees, int minutes, number seconds)
            : this(Math.Abs(circles), degrees, minutes, seconds, circles < 0) { }
        public DegreeAngle(int degrees, int minutes, number seconds)
            : this(0, Math.Abs(degrees), minutes, seconds, degrees < 0) { }
        public DegreeAngle(int minutes, number seconds)
            : this(0, 0, Math.Abs(minutes), seconds, minutes < 0) { }
        private DegreeAngle(number totalSeconds, bool validate)
        {
            if (validate)
                Assert.IsInRange(totalSeconds, MinTotalSeconds, MaxTotalSeconds, nameof(totalSeconds));
            TotalSeconds = totalSeconds;
            _circles = null;
            _degrees = null;
            _minutes = null;
            _seconds = null;
        }

        public number TotalSeconds { get; }
        public number TotalMinutes =>
            TotalSeconds / Constants.ArcsecondsPerArcminute;
        public number TotalDegrees =>
            TotalSeconds / Constants.ArcsecondsPerDegree;
        public bool IsNegative =>
            TotalSeconds < Constants.Zero;

        public int Circles =>
            EnsureCircles();
        public int Degrees =>
            EnsureDegrees();
        public int Minutes =>
            EnsureMinutes();
        public number Seconds =>
            EnsureSeconds();

        public Angle ToAngle() =>
            new Angle(TotalDegrees, AngleUnit.Degree);

        public RadianAngle ToRadianAngle() =>
            new RadianAngle(TotalSeconds * Constants.PI / (Constants.Half * Constants.DegreesPerTurn * Constants.ArcsecondsPerDegree));

        public DegreeAngle ToNormalized() =>
            new DegreeAngle(TotalSeconds % (Constants.DegreesPerTurn * Constants.ArcsecondsPerDegree));

        public bool IsZero() =>
            TotalSeconds == Constants.Zero;

        public override string ToString() =>
            DummyStaticFormatter.ToString<DegreeAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<DegreeAngle, AngleUnit>(formatProvider, this);

        number ILinearQuantity<AngleUnit>.BaseValue => TotalDegrees;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => AngleUnit.Degree;
        number ILinearQuantity<AngleUnit>.Value => ((ILinearQuantity<AngleUnit>)this).BaseValue;
        AngleUnit ILinearQuantity<AngleUnit>.Unit => ((ILinearQuantity<AngleUnit>)this).BaseUnit;

        public static DegreeAngle FromAngle(Angle angle) =>
            new DegreeAngle(angle.Convert(AngleUnit.Arcsecond).Value);

        private int EnsureCircles()
        {
            if (!_circles.HasValue)
                _circles = Math.Abs((int)(TotalDegrees / Constants.DegreesPerTurn));
            return _circles.Value;
        }

        private int EnsureDegrees()
        {
            if (!_degrees.HasValue)
                _degrees = Math.Abs((int)(TotalDegrees % Constants.DegreesPerTurn));
            return _degrees.Value;
        }

        private int EnsureMinutes()
        {
            if (!_minutes.HasValue)
                _minutes = Math.Abs((int)(TotalMinutes % Constants.ArcminutesPerDegree));
            return _minutes.Value;
        }

        private number EnsureSeconds()
        {
            if (!_seconds.HasValue)
                _seconds = Math.Abs(TotalSeconds % Constants.ArcsecondsPerArcminute);
            return _seconds.Value;
        }
    }
}