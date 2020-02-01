using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace QuantitativeWorld.Formatting
{
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly OrderedDictionary _inner;

        public OrderedDictionary()
            : this(new OrderedDictionary()) { }
        public OrderedDictionary(int capacity)
            : this(new OrderedDictionary(capacity)) { }
        public OrderedDictionary(IEqualityComparer<TKey> comparer)
            : this(new OrderedDictionary(new TypedEqualityComparerWrapper<TKey>(comparer))) { }
        private OrderedDictionary(OrderedDictionary inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public TValue this[TKey key]
        {
            get => (TValue)_inner[key];
            set => _inner[key] = value;
        }
        public TValue this[int index]
        {
            get => (TValue)_inner[index];
            set => _inner[index] = value;
        }

        public ICollection<TKey> Keys => GetKeys();
        public ICollection<TValue> Values => GetValues();

        public IList<TKey> GetKeys() => new ReadOnlyCollection<TKey>(_inner.Keys.Cast<TKey>().ToList());
        public IList<TValue> GetValues() => new ReadOnlyCollection<TValue>(_inner.Values.Cast<TValue>().ToList());

        public int Count => _inner.Count;
        public bool IsReadOnly => _inner.IsReadOnly;

        public void Add(TKey key, TValue value) => _inner.Add(key, value);
        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        public void Clear() => _inner.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) => ContainsKey(item.Key) && Equals(_inner[item.Key], item.Value);
        public bool ContainsKey(TKey key) => _inner.Contains(key);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            GetKeyValuePairs()
            .ToArray()
            .CopyTo(array, arrayIndex);

        public bool Remove(TKey key)
        {
            bool containedKey = _inner.Contains(key);
            _inner.Remove(key);
            return containedKey;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool containedItem = Contains(item);
            _inner.Remove(item.Key);
            return containedItem;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_inner.Contains(key))
            {
                value = (TValue)_inner[key];
                return true;
            }

            value = default(TValue);
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => GetKeyValuePairs().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<KeyValuePair<TKey, TValue>> GetKeyValuePairs() =>
            _inner.Keys.Cast<TKey>()
            .Select(k => new KeyValuePair<TKey, TValue>(k, (TValue)_inner[k]));

        class TypedEqualityComparerWrapper<T> : IEqualityComparer<T>, IEqualityComparer
        {
            private readonly IEqualityComparer<T> _inner;

            public TypedEqualityComparerWrapper(IEqualityComparer<T> comparer)
            {
                _inner = comparer ?? throw new ArgumentNullException(nameof(comparer));
            }

            bool IEqualityComparer<T>.Equals(T x, T y) => _inner.Equals(x, y);
            int IEqualityComparer<T>.GetHashCode(T obj) => _inner.GetHashCode(obj);

            bool IEqualityComparer.Equals(object x, object y) => _inner.Equals((T)x, (T)y);
            int IEqualityComparer.GetHashCode(object obj) => _inner.GetHashCode((T)obj);
        }
    }
}
