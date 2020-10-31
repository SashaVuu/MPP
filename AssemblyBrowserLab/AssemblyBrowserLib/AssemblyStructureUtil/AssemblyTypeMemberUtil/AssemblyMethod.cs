using AssemblyBrowserLib.SignatureUtil;
using System.Reflection;

namespace AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil
{
    public class AssemblyMethod : AssemblyTypeMember
    {
        private MethodInfo methodInfo;

        public AssemblyMethod(MethodInfo _methodInfo)
        {
            methodInfo = _methodInfo;
            Name = _methodInfo.Name;
            FullName = GetFullName();
        }

        protected override string GetFullName()
        {
            return Signature.GetMethodSignature(methodInfo);
        }
    }
}
