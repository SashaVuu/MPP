using AssemblyBrowserLib.SignatureUtil;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil
{
    public class AssemblyProperty : AssemblyTypeMember
    {
        private PropertyInfo propertyInfo;

        public string getAccessModifier;
        public string setAccessModifier;

        public AssemblyProperty(PropertyInfo _propertyInfo)
        {
            propertyInfo = _propertyInfo;
            Name = _propertyInfo.Name;
            FullName = GetFullName();
        }

        protected override string GetFullName()
        {
            string result = "";

            AccessModifier = AccessModifiers.GetAccessModifiers(propertyInfo.PropertyType);
            result += AccessModifier;

            DataAttribute= DataAttributes.GetDataAttributes(propertyInfo.PropertyType);
            result += DataAttribute;

            result += TypeName.GetTypeName(propertyInfo.PropertyType);
            result += propertyInfo.Name + "{ ";

            if (propertyInfo.CanRead)
            {
                getAccessModifier= AccessModifiers.GetAccessModifiers(propertyInfo.GetMethod);
                result += getAccessModifier + "get; ";
            }
            if (propertyInfo.CanWrite)
            {
                setAccessModifier= AccessModifiers.GetAccessModifiers(propertyInfo.SetMethod);
                result +=setAccessModifier + "set; ";
            }

            result += " }";
            return result;
        }
        
    }
}
