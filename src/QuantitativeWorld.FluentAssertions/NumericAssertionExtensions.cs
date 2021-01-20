using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Numeric;

namespace QuantitativeWorld.FluentAssertions
{
#if DECIMAL
    using number = System.Decimal;
#else
    using number = System.Double;
#endif

    public static class NumericAssertionExtensions
    {
        public static AndConstraint<NumericAssertions<Length>> BeApproximately(this NumericAssertions<Length> parent, Length expectedValue, Length precision, string because = "", params object[] becauseArgs)
        {
            string failMessage =
                ((Length?)parent.Subject).HasValue
                ? "Expected {context:Length} to be approximately {0} +/- {1}{reason}, but found {2} which differs by {3}."
                : "Expected {context:Length} to be approximately {0} +/- {1}{reason}, but found {2}.";
            var difference =
                (Length?)parent.Subject - expectedValue;
            Execute.Assertion
                .ForCondition(
                    ((Length?)parent.Subject).HasValue
                    && Length.Abs(difference) <= precision)
                .BecauseOf(because, becauseArgs)
                .FailWith(failMessage, expectedValue, precision, ((Length?)parent.Subject), difference);
            return new AndConstraint<NumericAssertions<Length>>(parent);
        }
        public static AndConstraint<NullableNumericAssertions<Length>> BeApproximately(this NullableNumericAssertions<Length> parent, Length? expectedValue, Length precision, string because = "", params object[] becauseArgs)
        {
            string failMessage =
                ((Length?)parent.Subject).HasValue
                ? "Expected {context:Length} to be approximately {0} +/- {1}{reason}, but found {2} which differs by {3}."
                : "Expected {context:Length} to be approximately {0} +/- {1}{reason}, but found {2}.";
            var difference =
                (Length?)parent.Subject - expectedValue;
            Execute.Assertion
                .ForCondition(
                    !((Length?)parent.Subject).HasValue && !expectedValue.HasValue
                    || Length.Abs(difference) <= precision)
                .BecauseOf(because, becauseArgs)
                .FailWith(failMessage, expectedValue, precision, ((Length?)parent.Subject), difference);
            return new AndConstraint<NullableNumericAssertions<Length>>(parent);
        }
        public static AndConstraint<NullableNumericAssertions<Length>> BeApproximately(this NullableNumericAssertions<Length> parent, Length expectedValue, Length precision, string because = "", params object[] becauseArgs)
        {
            string failMessage =
                ((Length?)parent.Subject).HasValue
                ? "Expected {context:Length} to be approximately {0} +/- {1}{reason}, but found {2} which differs by {3}."
                : "Expected {context:Length} to be approximately {0} +/- {1}{reason}, but found {2}.";
            var difference =
                (Length?)parent.Subject - expectedValue;
            Execute.Assertion
                .ForCondition(
                    ((Length?)parent.Subject).HasValue
                    && Length.Abs(difference) <= precision)
                .BecauseOf(because, becauseArgs)
                .FailWith(failMessage, expectedValue, precision, ((Length?)parent.Subject), difference);
            return new AndConstraint<NullableNumericAssertions<Length>>(parent);
        }

        public static AndConstraint<NumericAssertions<Length>> NotBeApproximately(this NumericAssertions<Length> parent, Length expectedValue, Length precision, string because = "", params object[] becauseArgs)
        {
            string failMessage =
                ((Length?)parent.Subject).HasValue
                ? "Did not expect {context:Length} to be approximately {0} +/- {1}{reason}, but found {2} which differs by {3}."
                : "Did not expect {context:Length} to be approximately {0} +/- {1}{reason}, but found {2}.";
            var difference =
                (Length?)parent.Subject - expectedValue;
            Execute.Assertion
                .ForCondition(
                    ((Length?)parent.Subject).HasValue
                    && Length.Abs(difference) > precision)
                .BecauseOf(because, becauseArgs)
                .FailWith(failMessage, expectedValue, precision, ((Length?)parent.Subject), difference);
            return new AndConstraint<NumericAssertions<Length>>(parent);
        }
        public static AndConstraint<NullableNumericAssertions<Length>> NotBeApproximately(this NullableNumericAssertions<Length> parent, Length? expectedValue, Length precision, string because = "", params object[] becauseArgs)
        {
            string failMessage =
                ((Length?)parent.Subject).HasValue
                ? "Did not expect {context:Length} to be approximately {0} +/- {1}{reason}, but found {2} which differs by {3}."
                : "Did not expect {context:Length} to be approximately {0} +/- {1}{reason}, but found {2}.";
            var difference =
                (Length?)parent.Subject - expectedValue;
            Execute.Assertion
                .ForCondition(
                    !((Length?)parent.Subject).HasValue && !expectedValue.HasValue
                    || Length.Abs(difference) > precision)
                .BecauseOf(because, becauseArgs)
                .FailWith(failMessage, expectedValue, precision, ((Length?)parent.Subject), difference);
            return new AndConstraint<NullableNumericAssertions<Length>>(parent);
        }
        public static AndConstraint<NullableNumericAssertions<Length>> NotBeApproximately(this NullableNumericAssertions<Length> parent, Length expectedValue, Length precision, string because = "", params object[] becauseArgs)
        {
            string failMessage =
                ((Length?)parent.Subject).HasValue
                ? "Did not expect {context:Length} to be approximately {0} +/- {1}{reason}, but found {2} which differs by {3}."
                : "Did not expect {context:Length} to be approximately {0} +/- {1}{reason}, but found {2}.";
            var difference =
                (Length?)parent.Subject - expectedValue;
            Execute.Assertion
                .ForCondition(
                    ((Length?)parent.Subject).HasValue
                    && Length.Abs(difference) > precision)
                .BecauseOf(because, becauseArgs)
                .FailWith(failMessage, expectedValue, precision, ((Length?)parent.Subject), difference);
            return new AndConstraint<NullableNumericAssertions<Length>>(parent);
        }
    }
}
