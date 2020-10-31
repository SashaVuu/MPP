using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil
{
    public abstract class AssemblyTypeMember
    {
        public string Name { get; set; } 
        public string FullName { get; set; }

        public string AccessModifier;

        public string DataAttribute;

        protected abstract string GetFullName();

    }
}
