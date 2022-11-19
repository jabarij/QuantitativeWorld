#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Tests
{
#else
namespace QuantitativeWorld.Json.Tests
{
#endif
    class SomeUnitOwner<T>
    {
        public T Unit { get; set; }
    }
}