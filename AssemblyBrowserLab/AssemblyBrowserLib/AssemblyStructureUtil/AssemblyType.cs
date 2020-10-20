using AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil;
using System;
using System.Collections.Generic;

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
        }

        private string GetFullName(Type type)
        {
            string result = type.Name;
            return result;
        }

    }
}
