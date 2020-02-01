using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace QuantitativeWorld.Formatting
{
    class CustomLengthFormatter : FormatterBase<Length>
    {
        private const string FormatTokenSeparator = "|";

        public override bool TryFormat(string format, Length length, IFormatProvider formatProvider, out string result)
        {
            bool isSuccess = TryGetFormatInfo(format, formatProvider, out var formatInfo);
            result =
                isSuccess
                ? Format(length, formatProvider, formatInfo)
                : null;
            return isSuccess;
        }

        private bool TryGetFormatInfo(string format, IFormatProvider formatProvider, out CustomLengthFormatInfo formatInfo)
        {
            Converter<Length, Length> lengthConverter = e => e;
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
                    lengthConverter = e => e.Convert(unit);
                else if (MatchesTokenBegin(format, index, "v", CanReadTokenUntilNextSeparator, out token))
                    valueFormat = token.Substring(1);
                else if (MatchesTokenBegin(format, index, "u", CanReadTokenUntilNextSeparator, out token))
                    unitFormat = token.Substring(1);
                else
                    token = new string(format[index], 1);

                index += token.Length;
            }

            formatInfo = new CustomLengthFormatInfo(lengthConverter, valueFormat, unitFormat);
            return true;
        }

        private static readonly OrderedDictionary<string, LengthUnit> _formattableUnits =
            LengthUnit.GetParsableUnits()
            .OrderByDescending(e => e.Abbreviation.Length)
            .Aggregate(
                seed: new OrderedDictionary<string, LengthUnit>(),
                func: (acc, e) =>
                {
                    acc.Add(e.Abbreviation, e);
                    return acc;
                });
        private static readonly IList<string> _formattableUnitKeys = _formattableUnits.GetKeys();
        private bool MatchesAnyUnitToken(string format, int position, out string token, out LengthUnit unit)
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
            unit = default(LengthUnit);
            return false;
        }

        private bool IsTokenSeparator(string format, int position, out string tokenSeparator) =>
            MatchesToken(format, position, FormatTokenSeparator, out tokenSeparator);

        private bool MatchesToken(string format, int position, string tokenCandidate, out string realToken)
        {
            if (position + tokenCandidate.Length <= format.Length
                && string.Equals(tokenCandidate, format.Substring(position, tokenCandidate.Length), StringComparison.Ordinal))
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
                && string.Equals(tokenCandidate, format.Substring(position, tokenCandidate.Length), StringComparison.Ordinal)
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
            new FormatException($"Unknown custom length format '{format}'.");

        private string Format(Length length, IFormatProvider formatProvider, CustomLengthFormatInfo formatInfo)
        {
            var formattedLength = formatInfo.LengthConverter(length);
            string valueStr = formattedLength.Value.ToString(formatInfo.ValueFormat, ResolveValueFormatProvider(formatProvider));
            string unitStr = formattedLength.Unit.ToString(formatInfo.UnitFormat, ResolveUnitFormatProvider(formatProvider));
            return string.Concat(valueStr, " ", unitStr);
        }
    }
}