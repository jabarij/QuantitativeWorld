#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class SpeedTests : TestsBase
    {
        public SpeedTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
