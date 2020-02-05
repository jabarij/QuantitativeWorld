using System.Reflection;

namespace QuantitativeWorld.DotNetExtensions
{
    public static class TypeExtensions
    {
        public static bool IsStatic(this MemberInfo member) =>
            (member.MetadataToken & (int)CallingConventions.HasThis) == (int)CallingConventions.HasThis;
    }
}
