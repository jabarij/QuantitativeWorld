using System.Reflection;

namespace QuantitativeWorld.DotNetExtensions
{
    internal static class TypeExtensions
    {
        public static bool IsStatic(this MemberInfo member) =>
            (member.MetadataToken & (int)CallingConventions.HasThis) == (int)CallingConventions.HasThis;
    }
}
