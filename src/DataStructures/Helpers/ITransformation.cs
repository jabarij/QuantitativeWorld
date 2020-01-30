namespace Plant.QAM.BusinessLogic.PublishedLanguage.Transformations
{
    public interface ITransformation<T>
    {
        T Transform(T value);
    }
}
