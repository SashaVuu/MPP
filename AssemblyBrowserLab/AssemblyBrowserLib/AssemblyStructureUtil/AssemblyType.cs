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
     
            FieldInfo[] fields = type.GetFields(flags).Where( item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute) ) == null).ToArray();
            foreach (var fieldInfo in fields)
            {
                typeMembers.Add(new AssemblyField(fieldInfo));
            }

            PropertyInfo[] properties = type.GetProperties(flags).Where(item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute)) == null).ToArray();
            foreach (var properyInfo in properties)
            {
                typeMembers.Add(new AssemblyProperty(properyInfo));
            }

            MethodInfo[] methods = type.GetMethods(flags).Where(item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute)) == null).ToArray();
            foreach (var methodInfo in type.GetMethods(flags))
            {
                 typeMembers.Add(new AssemblyMethod(methodInfo));
                
            }

        }

        private string GetFullName(Type type)
        {
            return Signature.GetTypeSignature(type);
        }

        

    }
}
