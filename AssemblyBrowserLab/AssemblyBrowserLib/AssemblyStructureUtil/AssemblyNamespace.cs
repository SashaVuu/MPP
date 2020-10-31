using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil
{
    public class AssemblyNamespace
    {

        public string FullName { get; set; }
        public List<AssemblyType> types { get; set; }

        public AssemblyNamespace(string typeName)
        {
            types = new List<AssemblyType>();
            this.FullName = typeName;
        }

        public void AddType(Type type)
        {
            types.Add(new AssemblyType(type));
        }


    }
}
