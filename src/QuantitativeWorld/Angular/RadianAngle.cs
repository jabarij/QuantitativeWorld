using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
    public partial struct RadianAngle : ILinearQuantity<AngleUnit>
    {
        private readonly static AngleUnit _unit = AngleUnit.Radian;

        public RadianAngle(decimal radians)
        {
            Radians = radians;
        }

        public decimal Radians { get; }

        decimal ILinearQuantity<AngleUnit>.BaseValue => Radians;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => _unit;
        decimal ILinearQuantity<AngleUnit>.Value => Radians;
        AngleUnit ILinearQuantity<AngleUnit>.Unit => _unit;

        public Angle ToAngle() =>
            new Angle(Radians, _unit);
        public RadianAngle ToNormalized() =>
            new RadianAngle(Radians % (2m * MathD.PI));

        public bool IsZero() =>
            Radians.Equals(decimal.Zero);

        public override string ToString() =>
            DummyStaticQuantityFormatter.ToString<RadianAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticQuantityFormatter.ToString<RadianAngle, AngleUnit>(formatProvider, this);

        public static RadianAngle FromAngle(Angle angle) =>
            new RadianAngle(angle.Convert(_unit).Value);
    }
}
