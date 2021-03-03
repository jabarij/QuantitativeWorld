#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class LengthTests : TestsBase
    {
        public LengthTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
