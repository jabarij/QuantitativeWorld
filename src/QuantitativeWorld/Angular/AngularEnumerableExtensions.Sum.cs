using QuantitativeWorld.DotNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantitativeWorld.Angular
{
    partial class AngularEnumerableExtensions
    {
        public static Angle Sum(this IEnumerable<Angle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(Angle), (acc, e) => acc + e);
        }

        public static Angle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Angle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(Angle), (acc, e) => acc + selector(e));
        }

        public static DegreeAngle Sum(this IEnumerable<DegreeAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(DegreeAngle), (acc, e) => acc + e);
        }

        public static DegreeAngle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, DegreeAngle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(DegreeAngle), (acc, e) => acc + selector(e));
        }

        public static RadianAngle Sum(this IEnumerable<RadianAngle> source)
        {
            Assert.IsNotNull(source, nameof(source));
            return source.Aggregate(default(RadianAngle), (acc, e) => acc + e);
        }

        public static RadianAngle Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, RadianAngle> selector)
        {
            Assert.IsNotNull(source, nameof(source));
            Assert.IsNotNull(selector, nameof(selector));
            return source.Aggregate(default(RadianAngle), (acc, e) => acc + selector(e));
        }
    }
}
