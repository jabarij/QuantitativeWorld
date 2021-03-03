#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Formatting
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Formatting
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class WeightUnitFormatterTests : TestsBase
    {
        public WeightUnitFormatterTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
