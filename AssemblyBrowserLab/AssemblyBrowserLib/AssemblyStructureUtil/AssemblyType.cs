using AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil;
using System;
using System.Collections.Generic;
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
        private void FillListOfTypeMembers(Type type) 
        {
            foreach (var fieldInfo in type.GetFields())
            {
                typeMembers.Add(new AssemblyField(fieldInfo));
            }

            foreach (var properyInfo in type.GetProperties())
            {
                typeMembers.Add(new AssemblyProperty(properyInfo));
            }

            foreach (var methodInfo in type.GetMethods())
            {
                 typeMembers.Add(new AssemblyMethod(methodInfo));
                
            }
        }

        private string GetFullName(Type type)
        {
            string result = GetAccessModifiers(type)+ GetAtributes(type)+ GetTypeOfClass(type)+type.Name;
            return result;
        }

        public static string GetAccessModifiers(Type type)
        {
            string result = "";
            if (type.IsPublic) { result = "public "; }
            if (type.IsNestedPrivate || type.IsNotPublic) { result = "private "; }
            if (type.IsNestedFamily) { result = "protected "; }
            if (type.IsNestedAssembly) { result = "internal "; }
            if (type.IsNestedFamORAssem) { result = "protected internal "; }
            return result;
        }

        public static string GetAtributes(Type type)
        {
            string result = "";
            if (type.IsAbstract) { result = "abstract "; }
            if (type.IsSealed) { result = "sealed "; }
            if (type.IsAbstract && type.IsSealed) { result = "static "; }
            return result;

        }

        private string GetTypeOfClass(Type type)
        {
            string result = "";
            if (type.IsClass) { result = "class "; }
            if (type.IsEnum) { result = "enum "; }
            if (type.IsInterface) { result = "interface "; }
            if (type.BaseType == typeof(MulticastDelegate)) { result = "delegate "; }
            return result;
        }
    }
}
