namespace QuantitativeWorld.DotNetExtensions
{
    interface ITransformation<T>
    {
        T Transform(T value);
    }
}
