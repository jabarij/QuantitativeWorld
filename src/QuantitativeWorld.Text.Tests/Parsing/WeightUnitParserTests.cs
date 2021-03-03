#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Parsing
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Parsing
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class WeightUnitParserTests : TestsBase
    {
        public WeightUnitParserTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
