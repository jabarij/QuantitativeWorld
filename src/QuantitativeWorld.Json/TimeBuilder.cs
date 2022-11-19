#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using number = System.Decimal;

#else
namespace QuantitativeWorld.Json;

using number = System.Double;
#endif

internal struct TimeBuilder
{
    public number? TotalSeconds;
    public int? Hours;
    public int? Minutes;
    public number? Seconds;
    public bool? IsNegative;

    public bool TryBuild(out Time result)
    {
        number? totalSeconds = TotalSeconds;
        if (totalSeconds.HasValue)
        {
            result = new Time(totalSeconds.Value);
            return true;
        }

        int? hours = Hours;
        int? minutes = Minutes;
        number? seconds = Seconds;
        bool? isNegative = IsNegative;
        if (hours.HasValue
            && minutes.HasValue
            && seconds.HasValue
            && isNegative.HasValue)
        {
            result = new Time(
                hours.Value, minutes.Value, seconds.Value, isNegative.Value);
            return true;
        }

        result = default(Time);
        return false;
    }
}