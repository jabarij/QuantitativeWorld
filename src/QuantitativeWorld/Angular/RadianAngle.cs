using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct RadianAngle : ILinearQuantity<AngleUnit>
    {
        private const double EmptyValue = 0d;
        private readonly double? _radians;

        public static readonly RadianAngle Zero = new RadianAngle(0d);

        public RadianAngle(double radians)
        {
            Assert.IsNotNaN(radians, nameof(radians));
            _radians = radians;
        }

        public double Radians =>
            _radians ?? EmptyValue;

        public Angle ToAngle() =>
            new Angle(Radians, AngleUnit.Radian);

        public DegreeAngle ToDegreeAngle() =>
            new DegreeAngle(Radians * 180d * 3600d / Math.PI);

        public RadianAngle ToNormalized() =>
            new RadianAngle(Radians % (2d * Math.PI));

        public bool IsZero() =>
            Radians.Equals(0d);

        public override string ToString() =>
            DummyStaticFormatter.ToString<RadianAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<RadianAngle, AngleUnit>(formatProvider, this);

        double ILinearQuantity<AngleUnit>.BaseValue => Radians;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => AngleUnit.Radian;
        double ILinearQuantity<AngleUnit>.Value => ((ILinearQuantity<AngleUnit>)this).BaseValue;
        AngleUnit ILinearQuantity<AngleUnit>.Unit => ((ILinearQuantity<AngleUnit>)this).BaseUnit;

        public static RadianAngle FromAngle(Angle angle) =>
            new RadianAngle(angle.Convert(AngleUnit.Radian).Value);

        public static implicit operator RadianAngle(double radians) =>
            new RadianAngle(radians);
    }
}
