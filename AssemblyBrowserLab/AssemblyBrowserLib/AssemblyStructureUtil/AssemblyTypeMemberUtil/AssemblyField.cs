using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil
{
    public class AssemblyField : AssemblyTypeMember
    {
  
        private FieldInfo fieldInfo;

        public AssemblyField(FieldInfo _fieldInfo)
        {
            fieldInfo = _fieldInfo;
            Name = _fieldInfo.Name;
            FullName = GetFullName();
        }


        protected override string GetFullName()
        {
            string result = fieldInfo.FieldType.Name + " " + Name;
            return result;
        }


    }
}
