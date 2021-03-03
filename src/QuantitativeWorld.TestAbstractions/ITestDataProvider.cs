#if DECIMAL
namespace DecimalQuantitativeWorld.TestAbstractions
{
#else
namespace QuantitativeWorld.TestAbstractions
{
#endif
    public interface ITestDataProvider
    {
        object[] GetTestParameters();
    }
}