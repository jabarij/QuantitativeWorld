#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class SpecificEnergyTests : TestsBase
    {
        public SpecificEnergyTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
