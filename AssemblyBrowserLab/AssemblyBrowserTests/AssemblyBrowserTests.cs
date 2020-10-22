using AssemblyBrowserLib.AssemblyStructureUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyBrowserTests
{
    [TestClass]
    public class AssemblyBrowserTests
    {
        AssemblyStructure assemblyStructure;

        [TestInitialize]
        public void Setup()
        {
            //Специально созданная сборка для тетстирования - "TestAssemblyLib.dll"
            AssemblyInfo.LoadAssemblyByPath("C:/Users/shell/source/repos/AssemblyBrowserLab/AssemblyBrowserLib/bin/Debug/netstandard2.0/AssemblyBrowserLib.dll");
            assemblyStructure=AssemblyInfo.assemblyStructure;
        }

        //----------------AccessModifiersTest----------------

        [TestMethod]
        public void PublicFieldModifierTest()
        {
            
        }

        [TestMethod]
        public void PrivtaeFieldModifierTest()
        {

        }

        [TestMethod]
        public void PublicPropertyModifierTest()
        {

        }

        [TestMethod]
        public void PublicClassModifierTest()
        {

        }

        [TestMethod]
        public void PrivateMethodModifierTest()
        {

        }

        [TestMethod]
        public void PublicStaticMethodModifierTest()
        {

        }


        //-------------Correct Structure------------------

        [TestMethod]
        public void AmountOfNamespacesInAssemblyTest()
        {

        }
        [TestMethod]
        public void AmountOfTypesInNamespaceTest()
        {

        }

        [TestMethod]
        public void AmountOfTypeMembersInTypeTest()
        {

        }

    }
}
