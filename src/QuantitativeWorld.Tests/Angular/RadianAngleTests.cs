using AutoFixture;
using QuantitativeWorld.Angular;
using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests.Angular
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public partial class RadianAngleTests : TestsBase
    {
        public RadianAngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private RadianAngle CreateRadianAngle() =>
            new RadianAngle(radians: Fixture.Create<number>());
    }
}
