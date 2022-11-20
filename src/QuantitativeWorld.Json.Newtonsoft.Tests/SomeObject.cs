#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Newtonsoft.Tests
{
#else
namespace QuantitativeWorld.Json.Newtonsoft.Tests
{
#endif
    class SomeUnitOwner<T>
    {
        public T Unit { get; set; }
    }
}