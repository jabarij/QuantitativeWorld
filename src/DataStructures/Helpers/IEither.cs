using System;

namespace Plant.QAM.BusinessLogic.PublishedLanguage.Monads
{
    public interface IEither<TLeft, TRight>
    {
        TResult Match<TResult>(Func<TLeft, TResult> mapLeft, Func<TRight, TResult> mapRight);
    }
}