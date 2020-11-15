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

        AssemblyType testAbstractClass;          


        [TestInitialize]
        public void Setup()
        {
            AssemblyInfo.LoadAssemblyByPath("C:/Users/shell/Documents/GitHub/MPP/AssemblyBrowserLab/AssemblyBrowserLib/bin/Debug/netstandard2.0/AssemblyBrowserLib.dll");
            assemblyStructure=AssemblyInfo.assemblyStructure;
            testNamespace = assemblyStructure.nameSpaces.Find(item => item.FullName.Contains("Tests"));
            testClass = testNamespace.types.Find(item => item.FullName.Contains("Test"));

            AssemblyNamespace testNamespace1 = assemblyStructure.nameSpaces.Find(item => item.FullName.Contains("AssemblyBrowserLib.AssemblyStructureUtil.AssemblyTypeMemberUtil"));
            testAbstractClass = testNamespace1.types.Find(item => item.FullName.Contains("TypeMember"));
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
            AssemblyTypeMember method= testClass.typeMembers.Find(item => item.FullName.Contains("internalVoidMethod"));
            Assert.AreEqual("internal ", method.AccessModifier);
 
        }

        [TestMethod]
        public void PrivateMethodModifierTest()
        {
            AssemblyTypeMember method = testClass.typeMembers.Find(item => item.FullName.Contains("privateIntMethod"));
            Assert.AreEqual("private ", method.AccessModifier);
        }

        //-------DataType
        [TestMethod]
        public void PublicClassTest()
        {
            Assert.AreEqual("public ", testClass.AccessModifier);
        }




        //-----------DataAttributeTests-------------
        [TestMethod]
        public void AbstractClassModifierTest()
        {
            Assert.AreEqual("abstract ", testAbstractClass.DataAttribute);
        }

        [TestMethod]
        public void StaticMethodModifierTest()
        {
            AssemblyTypeMember method = testClass.typeMembers.Find(item => item.FullName.Contains("staticVoidMethod"));
            Assert.AreEqual("static ", method.DataAttribute);
        }
      

        //-------------Correct Structure------------------

        [TestMethod]
        public void AmountOfNamespaces()
        {
            Assert.AreEqual(4, assemblyStructure.nameSpaces.Count);
        }

        [TestMethod]
        public void AmountOfTypesInNamespaceTest()
        {
            Assert.AreEqual(1, testNamespace.types.Count);
        }

        [TestMethod]
        public void AmountOfTypeMembersInTypeTest()
        {
            // 9 real  + 6 from Object
            Assert.AreEqual(15, testClass.typeMembers.Count);
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
