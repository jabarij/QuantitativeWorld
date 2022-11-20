#if DECIMAL
namespace DecimalQuantitativeWorld.Json;

using number = System.Decimal;
#else
namespace QuantitativeWorld.Json;

using number = System.Double;
#endif

internal struct AreaBuilder : ILinearQuantityBuilder<Area, AreaUnit>
{
    private number? _squareMetres;
    private number? _value;
    private AreaUnit? _unit;

    public AreaBuilder(Area area)
    {
        _squareMetres = area.SquareMetres;
        _value = area.Value;
        _unit = area.Unit;
    }

    public void SetBaseValue(number squareMetres)
    {
        _squareMetres = squareMetres;
        _value = null;
    }

    public void SetValue(number value)
    {
        _squareMetres = null;
        _value = value;
    }

    public void SetUnit(AreaUnit unit)
    {
        _unit = unit;
    }

    public bool TryBuild(out Area result, AreaUnit? defaultUnit = null)
    {
        number? squareMetres = _squareMetres;
        number? value = _value;
        AreaUnit? unit = _unit ?? defaultUnit;

        if (squareMetres.HasValue)
        {
            result = new Area(squareMetres.Value);
            if (unit.HasValue)
                result = result.Convert(unit.Value);
            return true;
        }

        if (value.HasValue && unit.HasValue)
        {
            result = new Area(value.Value, unit.Value);
            return true;
        }

        result = default(Area);
        return false;
    }
}