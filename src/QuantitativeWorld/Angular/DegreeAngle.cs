using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct DegreeAngle : ILinearQuantity<AngleUnit>
    {
        private const double MinTotalSeconds = double.MinValue;
        private const double MaxTotalSeconds = double.MaxValue;

        public const int MinDegrees = 0;
        public const int MaxDegrees = 359;
        public const int MinMinutes = 0;
        public const int MaxMinutes = 59;
        public const double MinSeconds = 0d;
        public const double ExclusiveMaxSeconds = 60d;

        public static readonly DegreeAngle Zero = new DegreeAngle(0d);
        public static readonly DegreeAngle PositiveInfinity = new DegreeAngle(double.PositiveInfinity, false);
        public static readonly DegreeAngle NegativeInfinity = new DegreeAngle(double.NegativeInfinity, false);

        private static readonly ValueRange<double> _secondsRange =
            new ValueRange<double>(MinSeconds, ExclusiveMaxSeconds, IntervalBoundaryType.Closed, IntervalBoundaryType.Open);

        private int? _circles;
        private int? _degrees;
        private int? _minutes;
        private double? _seconds;

        public DegreeAngle(double totalSeconds)
            : this(totalSeconds, true) { }
        public DegreeAngle(int circles, int degrees, int minutes, double seconds, bool isNegative)
        {
            Assert.IsGreaterThanOrEqual(circles, 0, nameof(circles));
            Assert.IsInRange(degrees, MinDegrees, MaxDegrees, nameof(degrees));
            Assert.IsInRange(minutes, MinMinutes, MaxMinutes, nameof(minutes));
            Assert.IsInRange(seconds, _secondsRange, nameof(seconds));
            double totalSeconds = (circles * Constants.ArcsecondsPerTurn + degrees * Constants.ArcsecondsPerDegree + minutes * Constants.ArcsecondsPerArcminute + seconds);
            if (isNegative)
                totalSeconds *= -1;
            Assert.IsInRange(totalSeconds, MinTotalSeconds, MaxTotalSeconds, nameof(totalSeconds));
            TotalSeconds = totalSeconds;
            _circles = circles;
            _degrees = degrees;
            _minutes = minutes;
            _seconds = seconds;
        }
        public DegreeAngle(int circles, int degrees, int minutes, double seconds)
            : this(Math.Abs(circles), degrees, minutes, seconds, circles < 0) { }
        public DegreeAngle(int degrees, int minutes, double seconds)
            : this(0, Math.Abs(degrees), minutes, seconds, degrees < 0) { }
        public DegreeAngle(int minutes, double seconds)
            : this(0, 0, Math.Abs(minutes), seconds, minutes < 0) { }
        private DegreeAngle(double totalSeconds, bool validate)
        {
            if (validate)
                Assert.IsInRange(totalSeconds, MinTotalSeconds, MaxTotalSeconds, nameof(totalSeconds));
            TotalSeconds = totalSeconds;
            _circles = null;
            _degrees = null;
            _minutes = null;
            _seconds = null;
        }

        public double TotalSeconds { get; }
        public double TotalMinutes =>
            TotalSeconds / Constants.ArcsecondsPerArcminute;
        public double TotalDegrees =>
            TotalSeconds / Constants.ArcsecondsPerDegree;
        public bool IsNegative =>
            TotalSeconds < 0d;

        public int Circles =>
            EnsureCircles();
        public int Degrees =>
            EnsureDegrees();
        public int Minutes =>
            EnsureMinutes();
        public double Seconds =>
            EnsureSeconds();

        public Angle ToAngle() =>
            new Angle(TotalDegrees, AngleUnit.Degree);

        public RadianAngle ToRadianAngle() =>
            new RadianAngle(TotalSeconds * Math.PI / (180d * 3600d));

        public DegreeAngle ToNormalized() =>
            new DegreeAngle(TotalSeconds % (360d * 60d * 60d));

        public bool IsZero() =>
            TotalSeconds == 0d;

        public override string ToString() =>
            DummyStaticFormatter.ToString<DegreeAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<DegreeAngle, AngleUnit>(formatProvider, this);

        double ILinearQuantity<AngleUnit>.BaseValue => TotalDegrees;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => AngleUnit.Degree;
        double ILinearQuantity<AngleUnit>.Value => ((ILinearQuantity<AngleUnit>)this).BaseValue;
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

        private double EnsureSeconds()
        {
            if (!_seconds.HasValue)
                _seconds = Math.Abs(TotalSeconds % Constants.ArcsecondsPerArcminute);
            return _seconds.Value;
        }
    }
}