#if DECIMAL
namespace DecimalQuantitativeWorld.Conversion.Tests
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Conversion.Tests
{
    using QuantitativeWorld.TestAbstractions;
#endif
    public partial class LinearUnitConverterTests : TestsBase
    {
        public LinearUnitConverterTests(TestFixture testFixture)
            : base(testFixture) { }
    }
}
