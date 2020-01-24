using System;

namespace Plant.QAM.BusinessLogic.PublishedLanguage.Monads
{
    public class Nothing
    {
        private Nothing() { if (Instance != null) throw new InvalidOperationException(); }
        public static readonly Nothing Instance = new Nothing();
    }
}