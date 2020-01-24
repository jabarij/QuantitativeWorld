using Plant.QAM.BusinessLogic.PublishedLanguage.Comparison;
using Plant.QAM.BusinessLogic.PublishedLanguage.Runtime;
using System;

namespace Plant.QAM.BusinessLogic.PublishedLanguage.Monads
{
    public struct Either<TLeft, TRight> : IEither<TLeft, TRight>, IEquatable<Either<TLeft, TRight>>
    {
        private readonly bool _isLeft;
        private readonly TLeft _left;
        private readonly TRight _right;

        public Either(TLeft left)
            : this(left, default(TRight), true) { }
        public Either(TRight right)
            : this(default(TLeft), right, false) { }
        private Either(TLeft left, TRight right, bool isLeft)
        {
            _left = left;
            _right = right;
            _isLeft = isLeft;
        }

        public T Match<T>(Func<TLeft, T> mapLeft, Func<TRight, T> mapRight) =>
            _isLeft
            ? mapLeft(_left)
            : mapRight(_right);

        public static Either<TLeft, TRight> Left(TLeft left) =>
            new Either<TLeft, TRight>(left, default(TRight), true);

        public static Either<TLeft, TRight> Right(TRight right) =>
            new Either<TLeft, TRight>(default(TLeft), right, false);

        public override string ToString() =>
            Match(
                mapLeft: l => $"Left({l?.ToString() ?? "[null]"})",
                mapRight: r => $"Right({r?.ToString() ?? "[null]"})");

        public static implicit operator Either<TLeft, TRight>(TLeft left) =>
            Left(left);

        public static implicit operator Either<TLeft, TRight>(TRight right) =>
            Right(right);

        #region Equality

        public bool Equals(Either<TLeft, TRight> other) =>
            Equals(_left, other._left)
            && Equals(_right, other._right)
            && _isLeft == other._isLeft;
        public override bool Equals(object obj) =>
            Equality.IsStructureEqualToObject(this, obj);
        public override int GetHashCode() =>
            new HashCode()
            .Append(_left, _right, _isLeft)
            .ToHashCode();

        public static bool operator ==(Either<TLeft, TRight> left, Either<TLeft, TRight> right) =>
            Equality.AreEqualStructures(left, right);
        public static bool operator !=(Either<TLeft, TRight> left, Either<TLeft, TRight> right) =>
            !(left == right);

        #endregion
    }

    public static class Either
    {
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft left, TRight right = default(TRight)) =>
             Either<TLeft, TRight>.Left(left);

        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight right, TLeft left = default(TLeft)) =>
            Either<TLeft, TRight>.Right(right);
    }
}
