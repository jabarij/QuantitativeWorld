using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct RadianAngle : ILinearQuantity<AngleUnit>
    {
        private const double EmptyValue = 0d;
        private readonly double? _radians;

        public RadianAngle(double radians)
        {
            _radians = radians;
        }

        public double Radians =>
            _radians ?? EmptyValue;

        public Angle ToAngle() =>
            new Angle((decimal)Radians, AngleUnit.Radian);

        public DegreeAngle ToDegreeAngle() =>
            new DegreeAngle(Radians * 180d * 3600d / Math.PI);

        public RadianAngle ToNormalized() =>
            new RadianAngle(Radians % (2d * Math.PI));

        public bool IsZero() =>
            Radians.Equals(0d);

        public override string ToString() =>
            DummyStaticQuantityFormatter.ToString<RadianAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticQuantityFormatter.ToString<RadianAngle, AngleUnit>(formatProvider, this);

        decimal ILinearQuantity<AngleUnit>.BaseValue => (decimal)Radians;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => AngleUnit.Radian;
        decimal ILinearQuantity<AngleUnit>.Value => ((ILinearQuantity<AngleUnit>)this).BaseValue;
        AngleUnit ILinearQuantity<AngleUnit>.Unit => ((ILinearQuantity<AngleUnit>)this).BaseUnit;

        public static RadianAngle FromAngle(Angle angle) =>
            new RadianAngle((double)angle.Convert(AngleUnit.Radian).Value);
    }
}
