using AutoFixture;

#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
    using number = System.Decimal;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
    using number = System.Double;
#endif

    public partial class TimeTests : TestsBase
    {
        public TimeTests(TestFixture testFixture)
            : base(testFixture) { }

        private Time CreateTime() =>
            new Time(totalSeconds: Fixture.Create<number>());
    }
}
