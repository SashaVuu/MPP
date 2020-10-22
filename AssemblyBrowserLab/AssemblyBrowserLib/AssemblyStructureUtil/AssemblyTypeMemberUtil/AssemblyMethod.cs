using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
            string parametrs = "()";

            string result="m_ ";
            if (methodInfo.IsStatic) { result += "static "; }
            if (methodInfo.IsPublic) { result += "public "; }
            else { result += "private "; }
            if (methodInfo.IsAbstract) { result += "abstract "; }
            result += methodInfo.ReturnType.Name+" " +methodInfo.Name + parametrs;

            return result;
        }
    }
}
