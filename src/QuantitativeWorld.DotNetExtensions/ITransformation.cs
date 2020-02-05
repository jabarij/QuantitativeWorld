namespace QuantitativeWorld.DotNetExtensions
{
    public interface ITransformation<T>
    {
        T Transform(T value);
    }
}
