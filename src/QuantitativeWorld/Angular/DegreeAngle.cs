using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct DegreeAngle : ILinearQuantity<AngleUnit>
    {
        public const int MinDegrees = 0;
        public const int MaxDegrees = 359;
        public const int MinMinutes = 0;
        public const int MaxMinutes = 59;
        public const double MinSeconds = 0d;
        public const double ExclusiveMaxSeconds = 60d;

        public static readonly DegreeAngle Zero = new DegreeAngle(0d);

        private const double EmptyValue = 0d;

        private static readonly AngleUnit _baseUnit = AngleUnit.Degree;
        private static readonly ValueRange<double> _secondsRange =
            new ValueRange<double>(MinSeconds, ExclusiveMaxSeconds, IntervalBoundaryType.Closed, IntervalBoundaryType.Open);

        private readonly double? _totalSeconds;

        public DegreeAngle(double totalSeconds)
        {
            Assert.IsNotNaN(totalSeconds, nameof(totalSeconds));
            _totalSeconds = totalSeconds;
        }
        public DegreeAngle(int circles, int degrees, int minutes, double seconds, bool isNegative)
        {
            Assert.IsGreaterThanOrEqual(circles, 0, nameof(circles));
            Assert.IsInRange(degrees, MinDegrees, MaxDegrees, nameof(degrees));
            Assert.IsInRange(minutes, MinMinutes, MaxMinutes, nameof(minutes));
            Assert.IsInRange(seconds, _secondsRange, nameof(seconds));
            double totalSeconds = (circles * 1296000 + degrees * 3600 + minutes * 60 + seconds);
            if (isNegative)
                totalSeconds *= -1;
            _totalSeconds = totalSeconds;
        }
        public DegreeAngle(int circles, int degrees, int minutes, double seconds)
            : this(Math.Abs(circles), degrees, minutes, seconds, circles < 0) { }
        public DegreeAngle(int degrees, int minutes, double seconds)
            : this(0, Math.Abs(degrees), minutes, seconds, degrees < 0) { }
        public DegreeAngle(int minutes, double seconds)
            : this(0, 0, Math.Abs(minutes), seconds, minutes < 0) { }

        public double TotalSeconds => _totalSeconds ?? EmptyValue;
        public double TotalMinutes =>
            TotalSeconds / 60d;
        public double TotalDegrees =>
            TotalSeconds / 3600d;
        public bool IsNegative =>
            TotalSeconds < 0d;

        public int Circles =>
            Math.Abs((int)(TotalDegrees / 360d));
        public int Degrees =>
            Math.Abs((int)(TotalDegrees % 360d));
        public int Minutes =>
            Math.Abs((int)(TotalMinutes % 60d));
        public double Seconds =>
            Math.Abs(TotalSeconds % 60d);

        public Angle ToAngle() =>
            new Angle((double)TotalDegrees, AngleUnit.Degree);

        public RadianAngle ToRadianAngle() =>
            new RadianAngle(TotalSeconds * Math.PI / (180d * 3600d));

        public DegreeAngle ToNormalized() =>
            new DegreeAngle(TotalSeconds % (360d * 60d * 60d));

        public bool IsZero() =>
            TotalSeconds.Equals(0d);

        public override string ToString() =>
            DummyStaticFormatter.ToString<DegreeAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<DegreeAngle, AngleUnit>(formatProvider, this);

        double ILinearQuantity<AngleUnit>.BaseValue => (double)TotalDegrees;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => AngleUnit.Degree;
        double ILinearQuantity<AngleUnit>.Value => ((ILinearQuantity<AngleUnit>)this).BaseValue;
        AngleUnit ILinearQuantity<AngleUnit>.Unit => ((ILinearQuantity<AngleUnit>)this).BaseUnit;

        public static DegreeAngle FromAngle(Angle angle) =>
            new DegreeAngle(angle.Convert(AngleUnit.Arcsecond).Value);
    }
}