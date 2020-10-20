using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil
{
    public abstract class AssemblyTypeMember
    {
        public string Name { get; set; } 
        public string FullName { get; set; }

        protected abstract string GetFullName();

    }
}
