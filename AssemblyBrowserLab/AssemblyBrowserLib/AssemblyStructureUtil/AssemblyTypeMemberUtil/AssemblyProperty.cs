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

        public AssemblyProperty(PropertyInfo _propertyInfo)
        {
            propertyInfo = _propertyInfo;
            Name = _propertyInfo.Name;
            FullName = GetFullName();
        }

        protected override string GetFullName()
        {
            return Signature.GetPropertySignature(propertyInfo);
        }

    }
}
