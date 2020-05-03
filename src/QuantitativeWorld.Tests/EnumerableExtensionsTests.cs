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
            Fixture.Customize<SomeUnit>(e => e.FromFactory(() => Fixture.CreateFromSet(new[] { SomeUnit.Unit, SomeUnit.Kilounit })));
            Fixture.Customize<SomeQuantity>(e => e.FromFactory(() => new SomeQuantity(Fixture.CreatePositive(), Fixture.Create<SomeUnit>())));
        }

        class TestObject<T>
        {
            public TestObject(T property)
            {
                Property = property;
            }

            public T Property { get; }
        }

        struct SomeUnit : ILinearUnit
        {
            public static readonly SomeUnit Unit = new SomeUnit(1d);
            public static readonly SomeUnit Kilounit = new SomeUnit(1000d);

            private SomeUnit(double valueInUnits)
            {
                ValueInUnits = valueInUnits;
            }

            public double ValueInUnits { get; }
            double ILinearUnit.ValueInBaseUnit => ValueInUnits;
        }

        struct SomeQuantity : ILinearQuantity<SomeUnit>
        {
            public SomeQuantity(double value, SomeUnit unit)
            {
                Value = value;
                Unit = unit;
            }

            double ILinearQuantity<SomeUnit>.BaseValue => Value / Unit.ValueInUnits;
            SomeUnit ILinearQuantity<SomeUnit>.BaseUnit => SomeUnit.Unit;
            public double Value { get; }
            public SomeUnit Unit { get; }
        }

        class SomeQuantityFactory : ILinearQuantityFactory<SomeQuantity, SomeUnit>
        {
            public static SomeQuantity Create(double value, SomeUnit unit) =>
                new SomeQuantity(value, unit);

            SomeQuantity ILinearQuantityFactory<SomeQuantity, SomeUnit>.Create(double value, SomeUnit unit) =>
                Create(value, unit);
        }
    }
}
