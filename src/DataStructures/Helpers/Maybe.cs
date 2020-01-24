using System;

namespace Plant.QAM.BusinessLogic.PublishedLanguage.Monads
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
    public struct Maybe<T> : IEither<T, Nothing>
    {
        private string DebuggerDisplay =>
            Match(
                mapLeft: e => $"Some({e})",
                mapRight: _ => $"None<{typeof(T).Name}>");

        private readonly Either<T, Nothing> _value;

        private Maybe(Either<T, Nothing> value)
        {
            _value = value;
        }

        public static Maybe<T> None() =>
            new Maybe<T>(Nothing.Instance);

        public static Maybe<T> Some(T value) =>
            new Maybe<T>(value);

        public static Maybe<T> Unit(T value) =>
            value?.Equals(default(T)) != false ? None() : Some(value);

        public TResult Match<TResult>(Func<T, TResult> mapLeft, Func<Nothing, TResult> mapRight) =>
            _value.Match(
                mapLeft: mapLeft,
                mapRight: mapRight);

        public static implicit operator Maybe<T>(Nothing nothing) =>
            None();
        public static implicit operator Maybe<T>(T value) =>
            Some(value);
        public static explicit operator T(Maybe<T> maybe) =>
            maybe.Match(mapLeft: e => e, mapRight: _ => throw new InvalidOperationException("Cannot reduce None to raw value."));
    }

    public static class Maybe
    {
        public static Maybe<T> Some<T>(T value) =>
            Maybe<T>.Some(value);

        public static Maybe<T> None<T>(T value = default(T)) =>
            Maybe<T>.None();

        public static Maybe<T> Resolve<T>(T? value) where T : struct =>
            value.HasValue ? Some(value.Value) : None(default(T));

        public static Maybe<T> Resolve<T>(T value) where T : class =>
            value != null ? Some(value) : None(value);
    }
}
