using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests
{
    public partial class EnumerableExtensionsTests : TestsBase
    {
        public EnumerableExtensionsTests(TestFixture testFixture)
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
