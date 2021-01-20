using AutoFixture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace QuantitativeWorld.TestAbstractions
{
#if DECIMAL
    using number = System.Decimal;
    using Constants = QuantitativeWorld.DecimalConstants;
#else
    using number = System.Double;
    using Constants = QuantitativeWorld.DoubleConstants;
#endif

    public class TestsBase : IClassFixture<TestFixture>
    {
        private readonly TestFixture _testFixture;

        public TestsBase(TestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        protected IFixture Fixture => _testFixture.Fixture;
        public const decimal DecimalPrecision = 0.000005m;
        public const decimal DecimalPrecisionPercentage = 0.0001m;
        public const double DoublePrecision = 0.000005d;
        public const double DoublePrecisionPercentage = 0.0001d;


        public static IEnumerable<object[]> GetTestData(Type sourceType, string dataSourceName)
        {
            var source = sourceType.GetMember(dataSourceName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.GetField);
            if (source == null || !source.Any())
                throw new InvalidOperationException($"Could not find member {sourceType.FullName}.{dataSourceName}.");
            if (source.Skip(1).Any())
                throw new InvalidOperationException($"Ambiguous member {sourceType.FullName}.{dataSourceName}.");
            var member = source.Single();

            object dataSource;
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    dataSource = ((FieldInfo)member).GetValue(null);
                    break;
                case MemberTypes.Method:
                    dataSource = ((MethodInfo)member).Invoke(null, null);
                    break;
                case MemberTypes.Property:
                    dataSource = ((PropertyInfo)member).GetValue(null);
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled value of {member.MemberType.GetType().FullName}.{member.MemberType} for member {sourceType.FullName}.{dataSourceName}.");
            }

            if (dataSource == null)
                return Enumerable.Empty<object[]>();

            if (dataSource is IEnumerable<ITestDataProvider> testDataProviders)
                return testDataProviders
                    .Select(e => e.GetTestParameters());
            else if (dataSource is IEnumerable enumerable)
                return enumerable
                    .Cast<object>()
                    .Select(e => new[] { e });
            else
                throw new InvalidOperationException($"Member {sourceType.FullName}.{dataSourceName} must return {typeof(IEnumerable<ITestDataProvider>).FullName} or {typeof(IEnumerable).FullName} to be a valid test data source.");
        }

        #region Length

        protected Length CreateLengthInUnit(LengthUnit unit) =>
            Fixture
            .Create<Length>()
            .Convert(unit);

        protected Length CreateLengthInUnitOtherThan(LengthUnit unitToExclude, params LengthUnit[] unitsToExclude) =>
            Fixture
            .Create<Length>()
            .Convert(CreateUnitOtherThan(unitToExclude, unitsToExclude));

        protected LengthUnit CreateUnitOtherThan(LengthUnit unitToExclude, params LengthUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(LengthUnit.GetPredefinedUnits().Except(new[] { unitToExclude }.Union(unitsToExclude)));

        #endregion

        #region Weight

        protected Weight CreateWeightInUnit(WeightUnit unit) =>
            Fixture
            .Create<Weight>()
            .Convert(unit);

        protected Weight CreateWeightInUnitOtherThan(WeightUnit unitToExclude, params WeightUnit[] unitsToExclude) =>
            Fixture
            .Create<Weight>()
            .Convert(CreateUnitOtherThan(unitToExclude, unitsToExclude));

        protected WeightUnit CreateUnitOtherThan(WeightUnit unitToExclude, params WeightUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(WeightUnit.GetPredefinedUnits().Except(new[] { unitToExclude }.Union(unitsToExclude)));

        #endregion

        #region Speed

        protected Speed CreateSpeedInUnit(SpeedUnit unit) =>
            Fixture
            .Create<Speed>()
            .Convert(unit);

        protected Speed CreateSpeedInUnitOtherThan(SpeedUnit unitToExclude, params SpeedUnit[] unitsToExclude) =>
            Fixture
            .Create<Speed>()
            .Convert(CreateUnitOtherThan(unitToExclude, unitsToExclude));

        protected SpeedUnit CreateUnitOtherThan(SpeedUnit unitToExclude, params SpeedUnit[] unitsToExclude) =>
            Fixture.CreateFromSet(SpeedUnit.GetPredefinedUnits().Except(new[] { unitToExclude }.Union(unitsToExclude)));

        #endregion
    }
}
