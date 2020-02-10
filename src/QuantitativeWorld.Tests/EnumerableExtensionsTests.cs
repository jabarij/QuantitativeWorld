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
            Fixture.Customize<Power>(e => e.FromFactory(() => new Power(Fixture.CreateGreaterThan(0m), Fixture.Create<PowerUnit>())));
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
            public static readonly PowerUnit Watt = new PowerUnit(1m);
            public static readonly PowerUnit Kilowatt = new PowerUnit(1000m);

            private PowerUnit(decimal valueInWatts)
            {
                ValueInWatts = valueInWatts;
            }

            public decimal ValueInWatts { get; }
            decimal ILinearUnit.ValueInBaseUnit => ValueInWatts;
        }

        struct Power : ILinearQuantity<PowerUnit>
        {
            public Power(decimal value, PowerUnit unit)
            {
                Value = value;
                Unit = unit;
            }

            public decimal Value { get; }
            public PowerUnit Unit { get; }
        }

        class PowerFactory : ILinearQuantityFactory<Power, PowerUnit>
        {
            public static Power Create(decimal value, PowerUnit unit) =>
                new Power(value, unit);

            Power ILinearQuantityFactory<Power, PowerUnit>.Create(decimal value, PowerUnit unit) =>
                Create(value, unit);
        }
    }
}
