using System;

namespace Plant.QAM.BusinessLogic.PublishedLanguage.Monads
{
    public static class MaybeExtensions
    {
        public static bool IsSome<T>(this Maybe<T> maybe) =>
            maybe.Match(
                mapLeft: _ => true,
                mapRight: _ => false);

        public static bool IsNone<T>(this Maybe<T> maybe) =>
            maybe.Match(
                mapLeft: _ => false,
                mapRight: _ => true);

        public static Maybe<T> AsMaybe<T>(this T? value)
            where T : struct =>
            value != null
            ? Maybe.Some(value.Value)
            : Maybe.None<T>();

        public static Maybe<TOut> Map<T, TOut>(this Maybe<T> maybe, Func<T, Maybe<TOut>> mapLeft, Func<Nothing, Maybe<TOut>> mapRight = null) =>
            maybe.Match(
                mapLeft: mapLeft,
                mapRight: mapRight ?? (_ => Maybe.None<TOut>()));

        public static T Reduce<T>(this Maybe<T> maybe, Func<T> defaultValue) =>
            maybe.Match(e => e, _ => defaultValue());

        public static T Reduce<T>(this Maybe<T> maybe, T defaultValue) =>
            maybe.Match(e => e, _ => defaultValue);

        public static T? Reduce<T>(this Maybe<T> maybe, T? defaultValue)
            where T : struct =>
            maybe.Match(e => e, _ => defaultValue);

        public static T? Reduce<T>(this Maybe<T> maybe, Func<T?> defaultValue)
            where T : struct =>
            maybe.Match(e => e, _ => defaultValue());

        public static T ReduceOrDefault<T>(this Maybe<T> maybe) =>
            Reduce(maybe, default(T));
    }
}
