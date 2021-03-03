namespace Common.Internals.DotNetExtensions
{
    internal interface ITransformation<T>
    {
        T Transform(T value);
    }
}
