#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Parsing
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Parsing
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class LengthUnitParserTests : TestsBase
    {
        public LengthUnitParserTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
