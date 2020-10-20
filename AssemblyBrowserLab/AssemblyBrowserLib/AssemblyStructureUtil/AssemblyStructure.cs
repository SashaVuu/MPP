using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil
{
    public class AssemblyStructure
    {
        public string FullName { get; set; } 
        public List<AssemblyNamespace> nameSpaces = new List<AssemblyNamespace>();

        public AssemblyStructure(Assembly assembly)
        {       
            foreach (Type type in assembly.GetTypes())
            {
                FullName = assembly.FullName;
                AssemblyNamespace assemblyNamespace = GetOrAddNamespace(type);
                assemblyNamespace.AddType(type);
            }
        }

        private AssemblyNamespace GetOrAddNamespace(Type type)
        {
            AssemblyNamespace assemblyNamespace;
            try
            {
                string namespaceName = type.Namespace;
                assemblyNamespace = new AssemblyNamespace(namespaceName);
                nameSpaces.Add(assemblyNamespace);
            }
            catch
            {
                assemblyNamespace = null;
            }

            return assemblyNamespace;
        }

    }
}
