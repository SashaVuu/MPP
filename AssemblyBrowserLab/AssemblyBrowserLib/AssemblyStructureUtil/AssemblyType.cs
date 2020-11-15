using AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil;
using AssemblyBrowserLib.SignatureUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowserLib.AssemblyStructureUtil
{
    public class AssemblyType
    {
       
        public string Name { get; set; }
        public string FullName { get; set; }

        public List<AssemblyTypeMember> typeMembers { get; set; }

        public string AccessModifier;

        public string DataAttribute;

        public string _TypeOfDataType;

        public AssemblyType(Type type)
        {
            typeMembers = new List<AssemblyTypeMember>();
            Name = type.Name;
            FullName = GetFullName(type);
            FillListOfTypeMembers(type);
        }

        //Заполняем список с полями,методами,свойствами
        private void FillListOfTypeMembers(Type type) 
        {
            var flags = BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic |
                        BindingFlags.Public ;

            //BindingFlags.DeclaredOnly

            FieldInfo[] fields = type.GetFields(flags).Where( item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute) ) == null && !item.IsSpecialName).ToArray();
            foreach (var fieldInfo in fields)
            {
                typeMembers.Add(new AssemblyField(fieldInfo));
            }

            PropertyInfo[] properties = type.GetProperties(flags).Where(item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute)) == null && !item.IsSpecialName).ToArray();
            foreach (var properyInfo in properties)
            {
                typeMembers.Add(new AssemblyProperty(properyInfo));
            }

            MethodInfo[] methods = type.GetMethods(flags).Where(item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute)) == null && !item.IsSpecialName).ToArray();
            foreach (var methodInfo in methods)
            {
                 typeMembers.Add(new AssemblyMethod(methodInfo));
                
            }

        }

        private string GetFullName(Type type)
        {
            string result = "";

            AccessModifier= AccessModifiers.GetAccessModifiers(type);
            result += AccessModifier;

            DataAttribute= DataAttributes.GetDataAttributes(type);
            result += DataAttribute;

            _TypeOfDataType= TypeOfDataType.GetTypeOfDataType(type);
            result += _TypeOfDataType;

            result += TypeName.GetTypeName(type);

            return result;
        }

        

    }
}
