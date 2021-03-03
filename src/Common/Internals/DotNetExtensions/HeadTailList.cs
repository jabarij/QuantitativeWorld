using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Internals.DotNetExtensions
{
    public class HeadTailList<T> : IReadOnlyList<T>
    {
        public static HeadTailList<T> Empty => new HeadTailList<T>();

        private readonly bool _hasHead;
        private readonly T _head;
        private readonly HeadTailList<T> _tail;

        public HeadTailList() { }
        public HeadTailList(T head)
        {
            _hasHead = true;
            _head = head;
            _tail = null;
            Count = 1;
        }
        public HeadTailList(T head, HeadTailList<T> tail)
        {
            _hasHead = true;
            _head = head;
            _tail =
                tail == null || tail.IsEmpty
                ? null
                : tail;
            Count = 1 + (tail?.Count ?? 0);
        }

        public T this[int index] =>
            GetItem(this, 0, index);

        public bool IsEmpty =>
            !_hasHead;
        public T Head =>
            _hasHead
            ? _head
            : throw new InvalidOperationException("Sequence contains no elements.");
        public HeadTailList<T> Tail =>
            _hasHead
            ? (_tail ?? Empty)
            : throw new InvalidOperationException("Sequence contains no elements.");
        public int Count { get; }

        public void Deconstruct(out T head, out HeadTailList<T> tail)
        {
            head = Head;
            tail = Tail;
        }

        public T[] ToArray() =>
            ToArray(this, new T[Count], 0);

        public static HeadTailList<T> operator +(T head, HeadTailList<T> tail) =>
            new HeadTailList<T>(head, tail);
        public static HeadTailList<T> operator +(HeadTailList<T> tail, T head) =>
            new HeadTailList<T>(head, tail);

        private static T[] ToArray(HeadTailList<T> list, T[] array, int currentIndex)
        {
            if (!list._hasHead)
                return array;

            array[currentIndex] = list._head;

            if (!(list._tail?._hasHead ?? false))
                return array;

            return ToArray(list._tail, array, currentIndex + 1);
        }

        private static T GetItem(HeadTailList<T> list, int currentIndex, int index)
        {
            var head =
                list._hasHead
                ? list._head
                : throw new ArgumentOutOfRangeException(nameof(index), index, "Argument is out of range.");

            if (currentIndex == index)
                return head;

            var tail =
                list._tail?._hasHead ?? false
                ? list._tail
                : throw new ArgumentOutOfRangeException(nameof(index), index, "Argument is out of range.");

            return GetItem(list._tail, currentIndex + 1, index);
        }

        public IEnumerator<T> GetEnumerator() =>
            new HeadTailListEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        class HeadTailListEnumerator : IEnumerator<T>
        {
            private HeadTailList<T> _original;
            private HeadTailList<T> _current;

            public HeadTailListEnumerator(HeadTailList<T> list)
            {
                _original = list;
                _current = null;
            }

            public T Current =>
                ThrowIfDisposed(() =>
                    _current != null
                    ? _current.Head
                    : throw new InvalidOperationException("Sequence contains no elements."));

            object IEnumerator.Current =>
                Current;

            public bool MoveNext()
            {
                ThrowIfDisposed();

                if (_current == null)
                {
                    _current = _original;
                    return true;
                }

                var tail = _current._tail;
                if (tail?._hasHead != true)
                    return false;

                _current = tail;
                return true;
            }

            public void Reset()
            {
                ThrowIfDisposed();
                _current = null;
            }

            #region IDisposable

            private bool _isDisposed;
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (_isDisposed)
                    return;

                if (disposing)
                {
                    _current = null;
                }

                _isDisposed = true;
            }
            private void ThrowIfDisposed(Action action = null)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(ToString());
                action?.Invoke();
            }
            private TResult ThrowIfDisposed<TResult>(Func<TResult> func) =>
                _isDisposed
                ? throw new ObjectDisposedException(ToString())
                : func();

            #endregion
        }
    }

    public static class HeadTailList
    {
        public static HeadTailList<T> Cons<T>(this T head, HeadTailList<T> tail) =>
            new HeadTailList<T>(head, tail);
    }
}
