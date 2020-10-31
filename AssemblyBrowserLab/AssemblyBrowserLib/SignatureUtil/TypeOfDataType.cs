using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyBrowserLib.SignatureUtil
{
    public static class TypeOfDataType
    {
        public static string GetTypeOfDataType(Type type)
        {
            string result = "";

            if (type.IsInterface) { result += "interface "; }
            else if (type.IsEnum) { result += "enum "; }
            else if (type.IsClass) { result += "class "; }

            return result;
        }
    }
}
