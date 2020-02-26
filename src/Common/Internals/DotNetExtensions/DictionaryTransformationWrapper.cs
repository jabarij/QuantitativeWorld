using System.Collections.Generic;

namespace QuantitativeWorld.DotNetExtensions
{
    internal class DictionaryTransformationWrapper<T> : ITransformation<T>
    {
        private readonly IDictionary<T, T> _dictionary;
        private readonly ITransformation<T> _transformation;

        public DictionaryTransformationWrapper(IDictionary<T, T> dictionary, ITransformation<T> transformation)
        {
            Assert.IsNotNull(dictionary, nameof(dictionary));
            Assert.IsNotNull(transformation, nameof(transformation));
            _dictionary = dictionary;
            _transformation = transformation;
        }

        public T Transform(T obj) =>
            _dictionary.TryGetValue(obj, out var result)
            ? result
            : _transformation.Transform(obj);
    }
}
