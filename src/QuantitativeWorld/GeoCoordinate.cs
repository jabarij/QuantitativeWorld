﻿using QuantitativeWorld.Angular;
using QuantitativeWorld.DotNetExtensions;
using System;

namespace QuantitativeWorld
{
    [System.Diagnostics.DebuggerDisplay("Geo coordinate(lat:{Latitude}, lon:{Longitude})")]
    public partial struct GeoCoordinate
    {
        private static readonly Length EarthRadius = new Length(metres: 6371000d);

        public static readonly double MinLatitude = -90d;
        public static readonly double MaxLatitude = 90d;
        public static readonly double MinLongitude = -180d;
        public static readonly double MaxLongitude = 180d;

        public static readonly DegreeAngle MinLatitudeDegrees = new DegreeAngle(-90d * 3600d);
        public static readonly DegreeAngle MaxLatitudeDegrees = new DegreeAngle(90d * 3600d);
        public static readonly DegreeAngle MinLongitudeDegrees = new DegreeAngle(-180d * 3600d);
        public static readonly DegreeAngle MaxLongitudeDegrees = new DegreeAngle(180d * 3600d);

        public static readonly RadianAngle MinLatitudeRadians = new RadianAngle(-Math.PI / 4d);
        public static readonly RadianAngle MaxLatitudeRadians = new RadianAngle(Math.PI / 4d);
        public static readonly RadianAngle MinLongitudeRadians = new RadianAngle(-Math.PI / 2d);
        public static readonly RadianAngle MaxLongitudeRadians = new RadianAngle(Math.PI / 2d);

        private static readonly ValueRange<DegreeAngle> LatitudeDegreesRange =
            new ValueRange<DegreeAngle>(MinLatitudeDegrees, MaxLatitudeDegrees);
        private static readonly ValueRange<DegreeAngle> LongitudeDegreesRange =
            new ValueRange<DegreeAngle>(MinLongitudeDegrees, MaxLongitudeDegrees);
        private static readonly ValueRange<RadianAngle> LatitudeRadiansRange =
            new ValueRange<RadianAngle>(MinLatitudeRadians, MaxLatitudeRadians);
        private static readonly ValueRange<RadianAngle> LongitudeRadiansRange =
            new ValueRange<RadianAngle>(MinLongitudeRadians, MaxLongitudeRadians);

        public static readonly GeoCoordinate Empty = new GeoCoordinate();

        private readonly DegreeAngle? _latitudeDegrees;
        private readonly DegreeAngle? _longitudeDegrees;
        private readonly RadianAngle? _latitudeRadians;
        private readonly RadianAngle? _longitudeRadians;

        public GeoCoordinate(double latitude, double longitude)
            : this(latitude, longitude, null) { }
        public GeoCoordinate(double latitude, double longitude, double? altitude)
        {
            Assert.IsInRange(latitude, MinLatitude, MaxLatitude, nameof(latitude));
            Assert.IsInRange(longitude, MinLongitude, MaxLongitude, nameof(longitude));
            if (altitude.HasValue)
                Assert.IsNotNaN(altitude.Value, nameof(altitude));

            var latitudeDegrees = new DegreeAngle(latitude * 3600d);
            var longitudeDegrees = new DegreeAngle(longitude * 3600d);
            _latitudeDegrees = latitudeDegrees;
            _longitudeDegrees = longitudeDegrees;
            _latitudeRadians = latitudeDegrees.ToRadianAngle();
            _longitudeRadians = longitudeDegrees.ToRadianAngle();
            Altitude = altitude;
        }
        public GeoCoordinate(DegreeAngle latitude, DegreeAngle longitude)
            : this(latitude, longitude, null) { }
        public GeoCoordinate(DegreeAngle latitude, DegreeAngle longitude, double? altitude)
        {
            Assert.IsInRange(latitude, LatitudeDegreesRange, nameof(latitude));
            Assert.IsInRange(longitude, LongitudeDegreesRange, nameof(longitude));
            if (altitude.HasValue)
                Assert.IsNotNaN(altitude.Value, nameof(altitude));

            _latitudeDegrees = latitude;
            _longitudeDegrees = longitude;
            _latitudeRadians = latitude.ToRadianAngle();
            _longitudeRadians = longitude.ToRadianAngle();
            Altitude = altitude;
        }
        public GeoCoordinate(RadianAngle latitude, RadianAngle longitude)
            : this(latitude, longitude, null) { }
        public GeoCoordinate(RadianAngle latitude, RadianAngle longitude, double? altitude)
        {
            Assert.IsInRange(latitude, LatitudeRadiansRange, nameof(latitude));
            Assert.IsInRange(longitude, LongitudeRadiansRange, nameof(longitude));
            if (altitude.HasValue)
                Assert.IsNotNaN(altitude.Value, nameof(altitude));

            _latitudeRadians = latitude;
            _longitudeRadians = longitude;
            _latitudeDegrees = latitude.ToDegreeAngle();
            _longitudeDegrees = longitude.ToDegreeAngle();
            Altitude = altitude;
        }

        public double Latitude => _latitudeDegrees?.TotalDegrees ?? double.NaN;
        public double Longitude => _longitudeDegrees?.TotalDegrees ?? double.NaN;
        public double? Altitude { get; }

        public RadianAngle LatitudeRadians => _latitudeRadians ?? default(RadianAngle);
        public RadianAngle LongitudeRadians => _longitudeRadians ?? default(RadianAngle);

        public DegreeAngle LatitudeDegrees => _latitudeDegrees ?? default(DegreeAngle);
        public DegreeAngle LongitudeDegrees => _longitudeDegrees ?? default(DegreeAngle);

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
            double a = MathA.Sin(Δφ / 2d) * MathA.Sin(Δφ / 2d) + MathA.Cos(φ1) * MathA.Cos(φ2) * MathA.Sin(Δλ / 2d) * MathA.Sin(Δλ / 2d);
            double c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

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

            double y = MathA.Sin(Δλ) * MathA.Cos(φ2);
            double x = MathA.Cos(φ1) * MathA.Sin(φ2) - MathA.Sin(φ1) * MathA.Cos(φ2) * MathA.Cos(Δλ);
            return new RadianAngle(Math.Atan2(y, x));
        }

        public GeoCoordinate GetDestinationPoint(Length distance, RadianAngle bearing)
        {
            if (IsEmpty)
                throw new ArgumentException("Cannot compute destination point from unknown coordinate.");

            var φ1 = LatitudeRadians;
            var λ1 = LongitudeRadians;
            var angularDistance = new RadianAngle((double)(distance / EarthRadius));

            var φ2 = new RadianAngle(Math.Asin(MathA.Sin(φ1) * MathA.Cos(angularDistance) + MathA.Cos(φ1) * MathA.Sin(angularDistance) * MathA.Cos(bearing)));

            double y = MathA.Sin(bearing) * MathA.Sin(angularDistance) * MathA.Cos(φ1);
            double x = MathA.Cos(angularDistance) - MathA.Sin(φ1) * MathA.Sin(φ2);
            var Δλ = new RadianAngle(Math.Atan2(y, x));
            var λ2 = λ1 + Δλ;

            return new GeoCoordinate(φ2, λ2);
        }
        public GeoCoordinate GetDestinationPoint(Length distance, DegreeAngle bearing) =>
            GetDestinationPoint(distance, bearing.ToRadianAngle());

        public override string ToString() =>
            DummyStaticFormatter.ToString(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString(formatProvider, this);
    }
}
