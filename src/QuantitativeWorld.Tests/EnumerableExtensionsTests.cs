using AutoFixture;
using QuantitativeWorld.Interfaces;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests
{
#if DECIMAL
    using Constants = QuantitativeWorld.DecimalConstants;
    using number = System.Decimal;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public partial class EnumerableExtensionsTests : TestsBase
    {
        public EnumerableExtensionsTests(TestFixture testFixture)
            : base(testFixture)
        {
            Fixture.Customize<SomeUnit>(e => e.FromFactory(() => Fixture.CreateFromSet(new[] { SomeUnit.Unit, SomeUnit.Kilounit })));
            Fixture.Customize<SomeQuantity>(e => e.FromFactory(() => new SomeQuantity(Fixture.CreatePositiveNumber(), Fixture.Create<SomeUnit>())));
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
            public static readonly SomeUnit Unit = new SomeUnit(Constants.One);
            public static readonly SomeUnit Kilounit = new SomeUnit((number)1000m);

            private SomeUnit(number valueInUnits)
            {
                ValueInUnits = valueInUnits;
            }

            public number ValueInUnits { get; }
            number ILinearUnit.ValueInBaseUnit => ValueInUnits;
        }

        struct SomeQuantity : ILinearQuantity<SomeUnit>
        {
            public SomeQuantity(number value, SomeUnit unit)
            {
                Value = value;
                Unit = unit;
            }

            number ILinearQuantity<SomeUnit>.BaseValue => Value / Unit.ValueInUnits;
            SomeUnit ILinearQuantity<SomeUnit>.BaseUnit => SomeUnit.Unit;
            public number Value { get; }
            public SomeUnit Unit { get; }
        }

        class SomeQuantityFactory : ILinearQuantityFactory<SomeQuantity, SomeUnit>
        {
            public static SomeQuantity Create(number value, SomeUnit unit) =>
                new SomeQuantity(value, unit);

            SomeQuantity ILinearQuantityFactory<SomeQuantity, SomeUnit>.Create(number value, SomeUnit unit) =>
                Create(value, unit);
        }
    }
}
