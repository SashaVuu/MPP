using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.SignatureUtil
{
    public static class AccessModifiers
    {
        
        /*
         * Type & Property Access Modifiers
         * https://docs.microsoft.com/en-us/dotnet/api/system.type.isnestedprivate?view=netcore-3.1
         */
        public static string GetAccessModifiers(Type type)
        {
            string result = "";

            if (type.IsNestedPrivate) { result += "private "; }
            else if (type.IsNestedAssembly) { result += "internal "; }
            else if (type.IsNestedFamily) { result += "protected "; }
            else if (type.IsNestedPublic) { result += "public "; }
            //else { result += "public "; }

            return result;
        }


        /*
         * Field & Method Access Modifiers
         * https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo.isfamilyorassembly?view=netcore-3.1 
         */
        public static string GetAccessModifiers(FieldInfo fieldInfo)
        {
            string result = "";

            if (fieldInfo.IsPrivate) { result += "private "; }
            else if (fieldInfo.IsAssembly) { result += "internal "; }
            else if (fieldInfo.IsFamily) { result += "protected "; }
            else if (fieldInfo.IsPublic) { result += "public "; }
            else { result += "public "; }

            return result;
        }

        public static string GetAccessModifiers(MethodInfo methodInfo)
        {
            string result = "";

            if (methodInfo.IsPrivate) { result += "private "; }
            else if (methodInfo.IsAssembly) { result += "internal "; }
            else if (methodInfo.IsFamily) { result += "protected "; }
            else if (methodInfo.IsPublic) { result += "public "; }
            else { result += "public "; }

            return result;
        }
    }
}
