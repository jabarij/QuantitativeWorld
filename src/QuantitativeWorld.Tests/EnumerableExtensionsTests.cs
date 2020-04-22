using AutoFixture;
using QuantitativeWorld.Interfaces;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests
{
    public partial class EnumerableExtensionsTests : TestsBase
    {
        public EnumerableExtensionsTests(TestFixture testFixture)
            : base(testFixture)
        {
            Fixture.Customize<PowerUnit>(e => e.FromFactory(() => Fixture.CreateFromSet(new[] { PowerUnit.Watt, PowerUnit.Kilowatt })));
            Fixture.Customize<Power>(e => e.FromFactory(() => new Power(Fixture.CreatePositive(), Fixture.Create<PowerUnit>())));
        }

        class TestObject<T>
        {
            public TestObject(T property)
            {
                Property = property;
            }

            public T Property { get; }
        }

        struct PowerUnit : ILinearUnit
        {
            public static readonly PowerUnit Watt = new PowerUnit(1d);
            public static readonly PowerUnit Kilowatt = new PowerUnit(1000d);

            private PowerUnit(double valueInWatts)
            {
                ValueInWatts = valueInWatts;
            }

            public double ValueInWatts { get; }
            double ILinearUnit.ValueInBaseUnit => ValueInWatts;
        }

        struct Power : ILinearQuantity<PowerUnit>
        {
            public Power(double value, PowerUnit unit)
            {
                Value = value;
                Unit = unit;
            }

            double ILinearQuantity<PowerUnit>.BaseValue => Value / Unit.ValueInWatts;
            PowerUnit ILinearQuantity<PowerUnit>.BaseUnit => PowerUnit.Watt;
            public double Value { get; }
            public PowerUnit Unit { get; }
        }

        class PowerFactory : ILinearQuantityFactory<Power, PowerUnit>
        {
            public static Power Create(double value, PowerUnit unit) =>
                new Power(value, unit);

            Power ILinearQuantityFactory<Power, PowerUnit>.Create(double value, PowerUnit unit) =>
                Create(value, unit);
        }
    }
}
