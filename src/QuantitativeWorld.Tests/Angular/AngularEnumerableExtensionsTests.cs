using QuantitativeWorld.TestAbstractions;

namespace QuantitativeWorld.Tests.Angular
{
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
