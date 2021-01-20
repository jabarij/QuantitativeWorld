namespace QuantitativeWorld.Tests
{
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
