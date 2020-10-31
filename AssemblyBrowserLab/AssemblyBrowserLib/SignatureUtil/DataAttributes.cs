using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.SignatureUtil
{
    public static class DataAttributes
    {
        public static string GetDataAttributes(Type type) 
        {
            string result = "";

            if (type.IsAbstract && type.IsSealed) { result += "static "; }
            else if (type.IsAbstract) { result += "abstract "; }
            else if (type.IsSealed) { result += "sealed "; }

            return result;

        }
        public static string GetDataAttributes(MethodInfo methodInfo)
        {
            string result = "";

            if (methodInfo.IsAbstract) { result += "abstract "; }
            else if (methodInfo.IsVirtual) { result += "virtual "; }
            else if (methodInfo.IsStatic) { result += "static "; }
           
            return result;
        }

    }
}
