#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Encoding
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Encoding
{
    using QuantitativeWorld.TestAbstractions;
#endif

    public partial class PolylineEncoderTests : TestsBase
    {
        public PolylineEncoderTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
