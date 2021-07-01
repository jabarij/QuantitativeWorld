#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class SpecificEnergyUnitTests : TestsBase
    {
        public SpecificEnergyUnitTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
