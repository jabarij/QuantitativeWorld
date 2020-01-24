using Plant.QAM.BusinessLogic.PublishedLanguage;

namespace DataStructures.Helpers
{
    public static class StringExtensions
    {
        public static string Multiply(this string value, int times)
        {
            Assert.IsGreaterThan(times, 0, nameof(times));

            char[] result = new char[value.Length * times];
            for (int time = 0; time < times; time++)
                for (int index = 0; index < value.Length; index++)
                    result[time * value.Length + index] = value[index];
            return new string(result);
        }
    }
}
