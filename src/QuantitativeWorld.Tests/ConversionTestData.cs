#if DECIMAL
namespace DecimalQuantitativeWorld.Tests
{
#else
namespace QuantitativeWorld.Tests
{
#endif
    public class ConversionTestData<T>
    {
        public ConversionTestData(T originalValue, T expectedValue)
        {
            OriginalValue = originalValue;
            ExpectedValue = expectedValue;
        }

        public T OriginalValue { get; set; }
        public T ExpectedValue { get; set; }
    }
}
