#if DECIMAL
namespace DecimalQuantitativeWorld.Json.Angular;

using DecimalQuantitativeWorld.Angular;
using number = System.Decimal;
#else
namespace QuantitativeWorld.Json.Angular;

using QuantitativeWorld.Angular;
using number = System.Double;
#endif

internal struct DegreeAngleBuilder
{
    private number? _totalSeconds;
    private int? _circles;
    private int? _degrees;
    private int? _minutes;
    private number? _seconds;
    private bool? _isNegative;

    public void SetTotalSeconds(number value)
        => _totalSeconds = value;

    public void SetCircles(int value)
        => _circles = value;

    public void SetDegrees(int value)
        => _degrees = value;

    public void SetMinutes(int value)
        => _minutes = value;

    public void SetSeconds(number value)
        => _seconds = value;

    public void SetIsNegative(bool value)
        => _isNegative = value;

    public bool TryBuild(out DegreeAngle result)
    {
        number? totalSeconds = _totalSeconds;
        if (totalSeconds.HasValue)
        {
            result = new DegreeAngle(totalSeconds.Value);
            return true;
        }

        int? circles = _circles;
        int? degrees = _degrees;
        int? minutes = _minutes;
        number? seconds = _seconds;
        bool? isNegative = _isNegative;
        if (circles.HasValue
            && degrees.HasValue
            && minutes.HasValue
            && seconds.HasValue
            && isNegative.HasValue)
        {
            result = new DegreeAngle(
                circles.Value, degrees.Value, minutes.Value, seconds.Value, isNegative.Value);
            return true;
        }

        result = default;
        return false;
    }
}