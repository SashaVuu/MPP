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
        
        //--------Field
        [TestMethod]
        public void PublicFieldModifierTest()
        {
            AssemblyTypeMember field = testClass.typeMembers.Find(item => item.FullName.Contains("publicBoolField"));
            Assert.AreEqual("public ", field.AccessModifier);
        }

        [TestMethod]
        public void PrivateFieldModifierTest()
        {
            AssemblyTypeMember field = testClass.typeMembers.Find(item => item.FullName.Contains("privateIntField"));
            Assert.AreEqual("private ", field.AccessModifier);
        }

        [TestMethod]
        public void ProtectedFieldModifierTest()
        {
            AssemblyTypeMember field = testClass.typeMembers.Find(item => item.FullName.Contains("protectedStringField"));
            Assert.AreEqual("protected ", field.AccessModifier);
        }


        //--------Property
        [TestMethod]
        public void GetSetPropertyModifierTest()
        {
            AssemblyProperty property = (AssemblyProperty)testClass.typeMembers.Find(item => item.FullName.Contains("Name"));
            Assert.AreEqual("public ", property.getAccessModifier);
            Assert.AreEqual("private ", property.setAccessModifier);
        }


        //--------Method
        [TestMethod]
        public void InternalMethodModifierTest()
        {
            AssemblyTypeMember method= testClass.typeMembers.Find(item => item.FullName.Contains("internalVoidMthod"));
            Assert.AreEqual("internal ", method.AccessModifier);
 
        }

        [TestMethod]
        public void PrivateMethodModifierTest()
        {
            AssemblyTypeMember method = testClass.typeMembers.Find(item => item.FullName.Contains("privateIntMethod"));
            Assert.AreEqual("private ", method.AccessModifier);
        }


        //-------------DataAttributesTests----------------



        //-------------Correct Structure------------------

        [TestMethod]
        public void AmountOfNamespaces()
        {
           // Assert.AreEqual(3, assemblyStructure.nameSpaces.Count);
        }

        [TestMethod]
        public void AmountOfTypesInNamespaceTest()
        {

        }

        [TestMethod]
        public void AmountOfTypeMembersInTypeTest()
        {

        }

        [TestMethod]
        public void MethodParamsTests()
        {
            AssemblyMethod method = (AssemblyMethod)testClass.typeMembers.Find(item => item.FullName.Contains("privateIntMethod"));
            Assert.AreEqual(2, method.paramsType.Count);
            Assert.AreEqual(true, method.paramsType.Contains("Int32 "));
            Assert.AreEqual(true, method.paramsType.Contains("Boolean "));
        }

    }
}
