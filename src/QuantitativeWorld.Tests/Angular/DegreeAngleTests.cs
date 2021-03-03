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

    public partial class DegreeAngleTests : TestsBase
    {
        public DegreeAngleTests(TestFixture testFixture)
            : base(testFixture) { }

        private DegreeAngle CreateDegreeAngle() =>
            new DegreeAngle(totalSeconds: Fixture.Create<number>());
    }
}
