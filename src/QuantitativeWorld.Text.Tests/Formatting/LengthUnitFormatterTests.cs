#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Formatting
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Formatting
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class LengthUnitFormatterTests : TestsBase
    {
        public LengthUnitFormatterTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
