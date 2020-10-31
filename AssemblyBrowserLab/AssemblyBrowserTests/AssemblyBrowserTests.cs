using AssemblyBrowserLib.AssemblyStructureUtil;
using AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace AssemblyBrowserTests
{
    [TestClass]
    public class AssemblyBrowserTests
    {
        AssemblyStructure assemblyStructure;
        AssemblyNamespace testNamespace;
        AssemblyType testClass;

        [TestInitialize]
        public void Setup()
        {
            AssemblyInfo.LoadAssemblyByPath("C:/Users/shell/Documents/GitHub/MPP/AssemblyBrowserLab/AssemblyBrowserLib/bin/Debug/netstandard2.0/AssemblyBrowserLib.dll");
            assemblyStructure=AssemblyInfo.assemblyStructure;
            testNamespace = assemblyStructure.nameSpaces.Find(item => item.FullName.Contains("Tests"));
            testClass = testNamespace.types[0];
        }

        //----------------AccessModifiersTest----------------

        [TestMethod]
        public void PublicFieldModifierTest()
        {
            AssemblyTypeMember field = testClass.typeMembers.Find(item => item.FullName.Contains("publicBoolField"));
            Assert.AreEqual("public Boolean publicBoolField", field.FullName);
        }

        [TestMethod]
        public void PrivateFieldModifierTest()
        {
            AssemblyTypeMember field = testClass.typeMembers.Find(item => item.FullName.Contains("privateIntField"));
            Assert.AreEqual("private Int32 privateIntField", field.FullName);
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
        public void AmountOfNamespaces()
        {
            Assert.AreEqual(3, assemblyStructure.nameSpaces.Count);
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
