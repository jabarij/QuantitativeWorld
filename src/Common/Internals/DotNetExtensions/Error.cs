using System;

namespace QuantitativeWorld.DotNetExtensions
{
    internal static class Error
    {
        public static Exception ArgumentIsOfUnexpectedType(object value, Type expectedType, string paramName) =>
            new ArgumentException($"Expected param to be of type {expectedType.FullName} but found {value.GetType().FullName}.", paramName);

        public static Exception ArgumentNotGreaterThan<T>(T value, T min, string paramName) =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be greater than {min}.");
        public static Exception ArgumentNotGreaterThanOrEqual<T>(T value, T min, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be greater than or equal to {min}.");
        public static Exception ArgumentNotLowerThan<T>(T value, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be lower than {max}.");
        public static Exception ArgumentNotLowerThanOrEqual<T>(T value, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be lower than or equal to {max}.");
        public static Exception ArgumentNotInRange<T>(T value, T min, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be inclusively in range {min}-{max}.");
        public static Exception ArgumentNotBetween<T>(T value, T min, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be between {min} and {max}.");

        public static Exception ArgumentNotNullOrGreaterThan<T>(T? value, T min, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be null or greater than {min}.");
        public static Exception ArgumentNotNullOrGreaterThanOrEqual<T>(T? value, T min, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be null or greater than or equal to {min}.");
        public static Exception ArgumentNotNullOrLowerThan<T>(T? value, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be null or lower than {max}.");
        public static Exception ArgumentNotNullOrLowerThanOrEqual<T>(T? value, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be null or lower than or equal to {max}.");
        public static Exception ArgumentNotNullOrInRange<T>(T? value, T min, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be null or inclusively in range {min}-{max}.");
        public static Exception ArgumentNotNullOrBetween<T>(T? value, T min, T max, string paramName) where T : struct =>
            new ArgumentOutOfRangeException(paramName, value, $"Expected param to be null or between {min} and {max}.");

        public static Exception ArgumentIsNull(string paramName) =>
            new ArgumentNullException(paramName);
        public static Exception ArgumentIsNullOrEmpty(string paramName) =>
            new ArgumentNullException(paramName);
        public static Exception ArgumentIsNullOrWhiteSpace(string paramName) =>
            new ArgumentNullException(paramName);

        public static Exception ArgumentIsNaN(string paramName) =>
            new ArgumentException("Param is not a number (NaN).", paramName);
        public static Exception ArgumentOutOfRange(string paramName, string message) =>
            new ArgumentOutOfRangeException(paramName, message);

        public static Exception IncorrectFormat(string message) =>
            new FormatException(message);
        public static Exception IncorrectFormat(string text, string formatName) =>
            new FormatException($"'{text}' is not a correct format of {formatName}.");
    }
}
