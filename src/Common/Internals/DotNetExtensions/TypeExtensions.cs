using System.Reflection;

namespace Common.Internals.DotNetExtensions
{
    internal static class TypeExtensions
    {
        public static bool IsStatic(this MemberInfo member) =>
            (member.MetadataToken & (int)CallingConventions.HasThis) == (int)CallingConventions.HasThis;
    }
}
