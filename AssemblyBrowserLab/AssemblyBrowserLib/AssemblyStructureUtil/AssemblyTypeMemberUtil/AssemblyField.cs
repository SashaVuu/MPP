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
            string result = Signature.GetFieldSignature(fieldInfo);
            return result;
        }

    }
}
