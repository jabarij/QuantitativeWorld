using QuantitativeWorld.DotNetExtensions;
using QuantitativeWorld.Interfaces;
using System;

namespace QuantitativeWorld.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public partial struct RadianAngle : ILinearQuantity<AngleUnit>
    {
        private const number MinRadians = number.MinValue;
        private const number MaxRadians = number.MaxValue;

        private const number EmptyValue = Constants.Zero;
        private readonly number? _radians;

        public static readonly RadianAngle Zero = new RadianAngle(Constants.Zero);
#if !DECIMAL
        public static readonly RadianAngle PositiveInfinity = new RadianAngle((number?)number.PositiveInfinity);
        public static readonly RadianAngle NegativeInfinity = new RadianAngle((number?)number.NegativeInfinity);
#endif

        public RadianAngle(number radians)
        {
            Assert.IsNotNaN(radians, nameof(radians));
            Assert.IsInRange(radians, MinRadians, MaxRadians, nameof(radians));
            _radians = radians;
        }
        private RadianAngle(number? radians)
        {
            _radians = radians;
        }

        public number Radians =>
            _radians ?? EmptyValue;

        public Angle ToAngle() =>
            new Angle(Radians, AngleUnit.Radian);

        public DegreeAngle ToDegreeAngle() =>
            new DegreeAngle(Radians * 180 * 3600 / Constants.PI);

        public RadianAngle ToNormalized() =>
            new RadianAngle(Radians % (2 * Constants.PI));

        public bool IsZero() =>
            Radians.Equals(Constants.Zero);

        public override string ToString() =>
            DummyStaticFormatter.ToString<RadianAngle, AngleUnit>(this);
        public string ToString(IFormatProvider formatProvider) =>
            DummyStaticFormatter.ToString<RadianAngle, AngleUnit>(formatProvider, this);

        number ILinearQuantity<AngleUnit>.BaseValue => Radians;
        AngleUnit ILinearQuantity<AngleUnit>.BaseUnit => AngleUnit.Radian;
        number ILinearQuantity<AngleUnit>.Value => ((ILinearQuantity<AngleUnit>)this).BaseValue;
        AngleUnit ILinearQuantity<AngleUnit>.Unit => ((ILinearQuantity<AngleUnit>)this).BaseUnit;

        public static RadianAngle FromAngle(Angle angle) =>
            new RadianAngle(angle.Convert(AngleUnit.Radian).Value);

        public static implicit operator RadianAngle(number radians) =>
            new RadianAngle(radians);
    }
}
