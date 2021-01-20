using QuantitativeWorld.Angular;
using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
    using Math = QuantitativeWorld.MathDecimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    [System.Diagnostics.DebuggerDisplay("Geo coordinate(lat: {Latitude}, lon: {Longitude})")]
    public partial struct GeoCoordinate
    {
        public static readonly DegreeAngle MinLatitude = -DegreeAngle.Right;
        public static readonly DegreeAngle MaxLatitude = DegreeAngle.Right;
        public static readonly DegreeAngle MinLongitude = -DegreeAngle.Straight;
        public static readonly DegreeAngle MaxLongitude = DegreeAngle.Straight;

        public static readonly GeoCoordinate Empty = new GeoCoordinate();
        public static readonly Length EarthRadius = new Length(metres: 6371008);

        private readonly DegreeAngle? _latitude;
        private readonly DegreeAngle? _longitude;
        private RadianAngle? _latitudeRadians;
        private RadianAngle? _longitudeRadians;

        public GeoCoordinate(DegreeAngle latitude, DegreeAngle longitude)
        {
            Assert.IsInRange(latitude, MinLatitude, MaxLatitude, nameof(latitude));
            Assert.IsInRange(longitude, MinLongitude, MaxLongitude, nameof(longitude));

            _latitude = latitude;
            _longitude = longitude;
            _latitudeRadians = latitude.ToRadianAngle();
            _longitudeRadians = longitude.ToRadianAngle();
        }
        public GeoCoordinate(RadianAngle latitude, RadianAngle longitude)
        {
            Assert.IsInRange(latitude, -RadianAngle.Right, RadianAngle.Right, nameof(latitude));
            Assert.IsInRange(longitude, -RadianAngle.Straight, RadianAngle.Straight, nameof(longitude));

            _latitudeRadians = latitude;
            _longitudeRadians = longitude;
            _latitude = latitude.ToDegreeAngle();
            _longitude = longitude.ToDegreeAngle();
        }
        public GeoCoordinate(number latitude, number longitude)
            : this(new DegreeAngle(latitude * 3600), new DegreeAngle(longitude * 3600)) { }

        public DegreeAngle Latitude => _latitude ?? default(DegreeAngle);
        public DegreeAngle Longitude => _longitude ?? default(DegreeAngle);

        public RadianAngle LatitudeRadians => EnsureLatitudeRadians();
        public RadianAngle LongitudeRadians => EnsureLongitudeRadians();

        public bool IsEmpty =>
            Equals(Empty);

        public Length GetDistanceTo(GeoCoordinate destination)
        {
            if (IsEmpty)
                throw new ArgumentException("Cannot compute distance from unknown coordinate.");
            if (destination.IsEmpty)
                throw new ArgumentException("Cannot compute distance to unknown coordinate.");

            var φ1 = LatitudeRadians;
            var λ1 = LongitudeRadians;
            var φ2 = destination.LatitudeRadians;
            var λ2 = destination.LongitudeRadians;
            var Δφ = φ2 - φ1;
            var Δλ = λ2 - λ1;
            number a = MathA.Sin(Δφ * Constants.Half) * MathA.Sin(Δφ * Constants.Half) + MathA.Cos(φ1) * MathA.Cos(φ2) * MathA.Sin(Δλ * Constants.Half) * MathA.Sin(Δλ * Constants.Half);
            number c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return c * EarthRadius;
        }

        public RadianAngle GetBearingTo(GeoCoordinate destination)
        {
            if (IsEmpty)
                throw new ArgumentException("Cannot compute bearing from unknown coordinate.");
            if (destination.IsEmpty)
                throw new ArgumentException("Cannot compute bearing to unknown coordinate.");

            var φ1 = LatitudeRadians;
            var λ1 = LongitudeRadians;
            var φ2 = destination.LatitudeRadians;
            var λ2 = destination.LongitudeRadians;
            var Δλ = λ2 - λ1;

            number y = MathA.Sin(Δλ) * MathA.Cos(φ2);
            number x = MathA.Cos(φ1) * MathA.Sin(φ2) - MathA.Sin(φ1) * MathA.Cos(φ2) * MathA.Cos(Δλ);
            return MathA.Atan2(y, x);
        }

        public GeoCoordinate GetDestinationPoint(Length distance, RadianAngle bearing)
        {
            if (IsEmpty)
                throw new ArgumentException("Cannot compute destination point from unknown coordinate.");

            var φ1 = LatitudeRadians;
            var λ1 = LongitudeRadians;
            var δ = new RadianAngle(distance / EarthRadius);

            var φ2 = MathA.Asin(MathA.Sin(φ1) * MathA.Cos(δ) + MathA.Cos(φ1) * MathA.Sin(δ) * MathA.Cos(bearing));

            number y = MathA.Sin(bearing) * MathA.Sin(δ) * MathA.Cos(φ1);
            number x = MathA.Cos(δ) - MathA.Sin(φ1) * MathA.Sin(φ2);
            var Δλ = MathA.Atan2(y, x);
            var λ2 = λ1 + Δλ;

            // Normalize longitude to range -π ... +π (that is, -180° ... +180°)
            λ2 = (λ2 + 3 * RadianAngle.PI) % (2 * Constants.PI) - RadianAngle.PI;

            return new GeoCoordinate(φ2, λ2);
        }
        public GeoCoordinate GetDestinationPoint(Length distance, DegreeAngle bearing) =>
            GetDestinationPoint(distance, bearing.ToRadianAngle());

        public override string ToString() =>
            DummyStaticFormatter.ToString(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString(formatProvider, this);

        private RadianAngle EnsureLatitudeRadians()
        {
            if (!_latitudeRadians.HasValue)
                _latitudeRadians = _latitude?.ToRadianAngle() ?? default(RadianAngle);
            return _latitudeRadians.Value;
        }
        private RadianAngle EnsureLongitudeRadians()
        {
            if (!_longitudeRadians.HasValue)
                _longitudeRadians = _longitude?.ToRadianAngle() ?? default(RadianAngle);
            return _longitudeRadians.Value;
        }
    }
}
