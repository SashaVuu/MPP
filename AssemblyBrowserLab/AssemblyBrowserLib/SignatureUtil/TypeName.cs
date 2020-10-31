using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.SignatureUtil
{
    public static class TypeName
    {

        public static string GetTypeName(Type type)
        {
            string result = "";

            if (type.IsGenericType)
            {
                result = GetGenericTypeName(type) + " ";
            }
            else
            {
                return type.Name + " ";
            }

            return result;
        }

        //Возвращает имя Generic типа List<List<kek>>
        private static string GetGenericTypeName(Type type)
        {
            string result = type.Name;
            result = result.Substring(0, result.LastIndexOf("`"));  //List

            result += "<";
            Type[] genericArgs = type.GetGenericArguments();
            for (int i = 0; i < genericArgs.Length; i++)
            {
                if (i != 0)
                {
                    result += ", ";
                }
                result += GetTypeName(genericArgs[i]);
            }
            result += ">";
            return result;
        }

    }
}
