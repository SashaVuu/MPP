using AssemblyBrowserLib.SignatureUtil;
using System.Reflection;

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
            string result = "";

            AccessModifier= AccessModifiers.GetAccessModifiers(fieldInfo);
            result += AccessModifier;
            DataAttribute = DataAttributes.GetDataAttributes(fieldInfo.FieldType);
            result += DataAttribute;

            result += TypeName.GetTypeName(fieldInfo.FieldType);
            result += fieldInfo.Name;

            return result;
        }

    }
}
