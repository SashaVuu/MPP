using AssemblyBrowserLib.SignatureUtil;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil
{
    public class AssemblyMethod : AssemblyTypeMember
    {
        private MethodInfo methodInfo;

        public List<string> paramsType;

        public AssemblyMethod(MethodInfo _methodInfo)
        {
            paramsType= new List<string>();
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

            if (parametrs.Length != 0)
            {
                for (int i = 0; i < parametrs.Length; i++)
                {
                    if (i != 0)
                    {
                        result += ", ";
                    }
                    paramsType.Add(TypeName.GetTypeName(parametrs[i].ParameterType));
                    result += paramsType[i] + parametrs[i].Name;
                }
            }
            result += " )";

            return result;
        }
    }
}
