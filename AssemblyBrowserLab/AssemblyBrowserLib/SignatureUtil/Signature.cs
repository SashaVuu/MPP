using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.SignatureUtil
{
    public static class Signature
    {

        //Возвращает заголовок поля
        public static string GetFieldSignature(FieldInfo fieldInfo)
        {
            string result = "";
            //public + int + kek ;
            result += AccessModifiers.GetAccessModifiers(fieldInfo);
            result += DataAttributes.GetDataAttributes(fieldInfo.FieldType);

            result += GetTypeName(fieldInfo.FieldType);
            result += fieldInfo.Name;

            return result;
        }

        //Возвращает заголовок свойства
        public static string GetPropertySignature(PropertyInfo propertyInfo)
        {
            string result = "";

            result = AccessModifiers.GetAccessModifiers(propertyInfo.PropertyType);
            result += DataAttributes.GetDataAttributes(propertyInfo.PropertyType);

            result += GetTypeName(propertyInfo.PropertyType);
            result += propertyInfo.Name + "{ ";

            if (propertyInfo.CanRead)
            {
                result += AccessModifiers.GetAccessModifiers(propertyInfo.GetMethod) + "get; ";
            }
            if (propertyInfo.CanWrite)
            {
                result += AccessModifiers.GetAccessModifiers(propertyInfo.SetMethod) + "set; ";
            }

            result += " }";
            return result;
        }


        //Возвращает заголовок метода
        public static string GetMethodSignature(MethodInfo methodInfo)
        {
            string result = "";

            result += AccessModifiers.GetAccessModifiers(methodInfo);            //public
            result += DataAttributes.GetDataAttributes(methodInfo);
            result += GetTypeName(methodInfo.ReturnType);                        // int
            result += methodInfo.Name;                                           // Kek
            result += "( ";

            ParameterInfo[] parametrs = methodInfo.GetParameters();
            for (int i = 0; i < parametrs.Length; i++)
            {
                if (i != 0)
                {
                    result += ", ";
                }
                result += GetTypeName(parametrs[i].ParameterType) + " " + parametrs[i].Name;
            }

            result += " )";

            return result;
        }

        //Возвращает заголовок типа данных (класс, структура и тд)
        public static string GetTypeSignature(Type type)
        {
            string result = "";

            result += AccessModifiers.GetAccessModifiers(type);
            result += DataAttributes.GetDataAttributes(type);
            result += TypeOfDataType.GetTypeOfDataType(type);

            result += GetTypeName(type);

            return result;
        }
        //----------------------------------------------------------------------------------------


        //Вовращает имя типа, даже если дженерик
        private static string GetTypeName(Type type)
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

        //Возвращает имя дженерик типа List<List<kek>>
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
