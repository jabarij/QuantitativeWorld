#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif
    public partial class GeoCoordinateTests : TestsBase
    {
        public GeoCoordinateTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
