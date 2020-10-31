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
            string result = "";

            AccessModifier = AccessModifiers.GetAccessModifiers(methodInfo);
            result += AccessModifier;

            DataAttribute= DataAttributes.GetDataAttributes(methodInfo);
            result += DataAttribute;

            result += TypeName.GetTypeName(methodInfo.ReturnType);    
            
            result += methodInfo.Name;                                          
            result += "( ";

            ParameterInfo[] parametrs = methodInfo.GetParameters();
            for (int i = 0; i < parametrs.Length; i++)
            {
                if (i != 0)
                {
                    result += ", ";
                }
                result += TypeName.GetTypeName(parametrs[i].ParameterType) + " " + parametrs[i].Name;
            }

            result += " )";

            return result;
        }
    }
}
