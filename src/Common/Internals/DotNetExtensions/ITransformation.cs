namespace QuantitativeWorld.DotNetExtensions
{
    internal interface ITransformation<T>
    {
        T Transform(T value);
    }
}
