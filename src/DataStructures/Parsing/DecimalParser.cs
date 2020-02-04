namespace QuantitativeWorld.Parsing
{
    public class DecimalParser : IParser<decimal>
    {
        public decimal Parse(string value) =>
            decimal.Parse(value);

        public bool TryParse(string value, out decimal result) =>
            decimal.TryParse(value, out result);
    }
}
