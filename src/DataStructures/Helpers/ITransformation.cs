namespace Plant.QAM.BusinessLogic.PublishedLanguage.Transformations
{
    interface ITransformation<T>
    {
        T Transform(T value);
    }
}
