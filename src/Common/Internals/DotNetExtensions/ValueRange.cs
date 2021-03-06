﻿using System;

namespace Common.Internals.DotNetExtensions
{
    [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
    internal struct ValueRange<TValue> : IEquatable<ValueRange<TValue>>
        where TValue : struct, IComparable<TValue>
    {
        public const IntervalBoundaryType DefaultBoundaryType = IntervalBoundaryType.Closed;
        public static readonly ValueRange<TValue> Empty = new ValueRange<TValue>();

        private string DebuggerDisplay =>
            string.Concat(
                "ValueRange: ",
                LeftBoundaryType == IntervalBoundaryType.Closed ? "[" : "(",
                From.ToString(),
                ";",
                To.ToString(),
                RightBoundaryType == IntervalBoundaryType.Closed ? "]" : ")");

        private readonly IntervalBoundaryType? _leftBoundaryType;
        private readonly IntervalBoundaryType? _rightBoundaryType;

        public ValueRange(
            TValue from,
            TValue to,
            IntervalBoundaryType leftBoundaryType = DefaultBoundaryType,
            IntervalBoundaryType rightBoundaryType = DefaultBoundaryType)
        {
            if (from.CompareTo(to) > 0)
                throw new ArgumentException("From value cannot be greater than to value.", nameof(from));

            From = from;
            To = to;
            _leftBoundaryType = leftBoundaryType;
            _rightBoundaryType = rightBoundaryType;
        }

        public TValue From { get; }
        public TValue To { get; }
        public IntervalBoundaryType LeftBoundaryType => _leftBoundaryType ?? DefaultBoundaryType;
        public IntervalBoundaryType RightBoundaryType => _rightBoundaryType ?? DefaultBoundaryType;

        public bool Contains(TValue value) =>
            IsInLeftBoundary(value)
            && IsInRightBoundary(value);
        private bool IsInLeftBoundary(TValue value)
        {
            switch (LeftBoundaryType)
            {
                case IntervalBoundaryType.Open:
                    return From.CompareTo(value) < 0;
                case IntervalBoundaryType.Closed:
                    return From.CompareTo(value) <= 0;
                default:
                    throw new NotImplementedException($"Handling {LeftBoundaryType.GetType().FullName}.{LeftBoundaryType} is not implemented.");
            }
        }
        private bool IsInRightBoundary(TValue value)
        {
            switch (RightBoundaryType)
            {
                case IntervalBoundaryType.Open:
                    return To.CompareTo(value) > 0;
                case IntervalBoundaryType.Closed:
                    return To.CompareTo(value) >= 0;
                default:
                    throw new NotImplementedException($"Handling {RightBoundaryType.GetType().FullName}.{RightBoundaryType} is not implemented.");
            }
        }

        public static bool operator ==(ValueRange<TValue> left, ValueRange<TValue> right) =>
            left.Equals(right);
        public static bool operator !=(ValueRange<TValue> left, ValueRange<TValue> right) =>
            !left.Equals(right);

        public bool Equals(ValueRange<TValue> other) =>
            Equals(From, other.From)
            && Equals(To, other.To)
            && LeftBoundaryType.Equals(other.LeftBoundaryType)
            && RightBoundaryType.Equals(other.RightBoundaryType);
        public override bool Equals(object obj) =>
            !ReferenceEquals(obj, null)
            && obj is ValueRange<TValue> other
            && Equals(other);
        public override int GetHashCode() =>
            new HashCode()
            .Append(From, To, LeftBoundaryType, RightBoundaryType)
            .CurrentHash;
    }
}
