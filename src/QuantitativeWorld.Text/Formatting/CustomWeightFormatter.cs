using Common.Internals.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

#if DECIMAL
namespace DecimalQuantitativeWorld.Text.Formatting
{
#else
namespace QuantitativeWorld.Text.Formatting
{
#endif
    class CustomWeightFormatter : FormatterBase<Weight>
    {
        private const string FormatTokenSeparator = "|";

        private static readonly OrderedDictionary<string, WeightUnit> _formattableUnits =
            WeightUnit.GetPredefinedUnits()
            .OrderByDescending(e => e.Abbreviation.Length)
            .Aggregate(
                seed: new OrderedDictionary<string, WeightUnit>(),
                func: (acc, e) =>
                {
                    acc.Add(e.Abbreviation, e);
                    return acc;
                });
        private static readonly IList<string> _formattableUnitKeys = _formattableUnits.GetKeys();

        public override bool TryFormat(string format, Weight weight, IFormatProvider formatProvider, out string result)
        {
            bool isSuccess = TryGetFormatInfo(format, formatProvider, out var formatInfo);
            result =
                isSuccess
                ? Format(weight, formatProvider, formatInfo)
                : null;
            return isSuccess;
        }

        private bool TryGetFormatInfo(string format, IFormatProvider formatProvider, out CustomWeightFormatInfo formatInfo)
        {
            Converter<Weight, Weight> weightConverter = e => e;
            string valueFormat = "0.########";
            string unitFormat = "s";

            int index = 0;
            while (index < format.Length)
            {
                string token;
                if (IsTokenSeparator(format, index, out string separator))
                {
                    index += separator.Length;
                    continue;
                }
                else if (MatchesAnyUnitToken(format, index, out token, out var unit))
                    weightConverter = e => e.Convert(unit);
                else if (MatchesToken(format, index, "lbs", out token))
                    weightConverter = e => e.Convert(WeightUnit.Pound);
                else if (MatchesTokenBegin(format, index, "v", CanReadTokenUntilNextSeparator, out token))
                    valueFormat = token.Substring(1);
                else if (MatchesTokenBegin(format, index, "u", CanReadTokenUntilNextSeparator, out token))
                    unitFormat = token.Substring(1);
                else
                    token = new string(format[index], 1);

                index += token.Length;
            }

            formatInfo = new CustomWeightFormatInfo(weightConverter, valueFormat, unitFormat);
            return true;
        }

        private bool IsTokenSeparator(string format, int position, out string tokenSeparator) =>
            MatchesToken(format, position, FormatTokenSeparator, out tokenSeparator);

        private bool MatchesToken(string format, int position, string tokenCandidate, out string realToken)
        {
            if (position + tokenCandidate.Length <= format.Length
                && format.ContainsAt(tokenCandidate, position))
            {
                realToken = tokenCandidate;
                return true;
            }

            realToken = null;
            return false;
        }

        private bool MatchesTokenBegin(string format, int position, string tokenCandidate, TokenReader canReadToken, out string realToken)
        {
            if (position + tokenCandidate.Length <= format.Length
                && format.ContainsAt(tokenCandidate, position)
                && canReadToken(format, position, out realToken))
                return true;

            realToken = null;
            return false;
        }

        private IFormatProvider ResolveValueFormatProvider(IFormatProvider formatProvider) =>
            formatProvider?.GetFormatProvider<NumberFormatInfo>()
            ?? formatProvider?.GetFormatProvider<CultureInfo>()
            ?? formatProvider;

        private IFormatProvider ResolveUnitFormatProvider(IFormatProvider formatProvider) =>
            formatProvider?.GetFormatProvider<CultureInfo>()
            ?? formatProvider;

        private delegate bool TokenReader(string format, int position, out string token);

        private bool CanReadTokenUntilNextSeparator(string format, int position, out string token)
        {
            var tokenBuilder = new StringBuilder();
            while (position < format.Length
                && !IsTokenSeparator(format, position, out string _))
            {
                tokenBuilder.Append(format[position]);
                position++;
            }

            token = tokenBuilder.ToString();
            position += token.Length;
            return true;
        }

        protected internal override FormatException GetUnknownFormatException(string format) =>
            new FormatException($"Unknown custom weight format '{format}'.");

        private string Format(Weight weight, IFormatProvider formatProvider, CustomWeightFormatInfo formatInfo)
        {
            var formattedWeight = formatInfo.WeightConverter(weight);
            string valueStr = formattedWeight.Value.ToString(formatInfo.ValueFormat, ResolveValueFormatProvider(formatProvider));

            var unitFormatter = formatProvider.GetFormat<ICustomFormatter<WeightUnit>>();
            string unitStr = unitFormatter != null
                ? unitFormatter.Format(formatInfo.UnitFormat, formattedWeight.Unit, ResolveUnitFormatProvider(formatProvider))
                : formattedWeight.Unit.ToString();

            return string.Concat(valueStr, " ", unitStr);
        }

        private bool MatchesAnyUnitToken(string format, int position, out string token, out WeightUnit unit)
        {
            for (int index = 0; index < _formattableUnitKeys.Count; index++)
            {
                string currentToken = _formattableUnitKeys[index];
                if (MatchesToken(format, position, currentToken, out var realToken))
                {
                    token = realToken;
                    unit = _formattableUnits[index];
                    return true;
                }
            }

            token = null;
            unit = default(WeightUnit);
            return false;
        }
    }
}