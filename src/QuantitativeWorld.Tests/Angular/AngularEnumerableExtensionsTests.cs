#if DECIMAL
namespace DecimalQuantitativeWorld.Tests.Angular
{
    using DecimalQuantitativeWorld.TestAbstractions;
#else
namespace QuantitativeWorld.Tests.Angular
{
    using QuantitativeWorld.TestAbstractions;
#endif
    public partial class AngularEnumerableExtensionsTests : TestsBase
    {
        public AngularEnumerableExtensionsTests(TestFixture testFixture)
            : base(testFixture) { }

        class TestObject<T>
        {
            public TestObject(T property)
            {
                Property = property;
            }

            public T Property { get; }
        }
    }
}
