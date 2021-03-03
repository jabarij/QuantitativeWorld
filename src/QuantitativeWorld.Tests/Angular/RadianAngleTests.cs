using AutoFixture;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.Angular;
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.Angular;
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    public partial class RadianAngleTests : TestsBase
    {
        public RadianAngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private RadianAngle CreateRadianAngle() =>
            new RadianAngle(radians: Fixture.Create<number>());
    }
}
