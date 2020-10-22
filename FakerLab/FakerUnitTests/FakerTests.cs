using FakerLab.TestClasses;
using FakerLib.FakerUtil;
using FakerLib.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FakerUnitTests
{
    [TestClass]
    public class FakerTests
    {
        Faker faker;

        [TestInitialize]
        public void Setup()
        {
            faker = new Faker();
        }

        // --------------List Tests--------------
        [TestMethod]
        public void ListOfIntGenerationTest()
        {
            List<int> list1 = faker.Create<List<int>>();
            Assert.AreEqual(typeof(int),list1[0].GetType());
            Assert.AreNotEqual(null, list1);
            Assert.AreNotEqual(0, list1[0]);
            Assert.AreNotEqual(0, list1.Count);
        }

        [TestMethod]
        public void ListOfPersonalClassGenerationTest()
        {
            List<A> list1 = faker.Create<List<A>>();
            Assert.AreEqual(typeof(A), list1[0].GetType());
            Assert.AreNotEqual(null, list1);
            Assert.AreNotEqual(null, list1[0]);
            Assert.AreNotEqual(0, list1.Count);
        }

        [TestMethod]
        public void ListOfListOfIntGenerationTest()
        {
            List<List<int>> list1 = faker.Create<List<List<int>>>();
            Assert.AreEqual(typeof(List<int>), list1[0].GetType());
            Assert.AreEqual(typeof(int), list1[0][0].GetType());
            Assert.AreNotEqual(0, list1[0].Count);
            Assert.AreNotEqual(0, list1.Count);
            Assert.AreNotEqual(0, list1[0][0]);

        }

        [TestMethod]
        public void ListOfListOfClassGenerationTest()
        {
            List<List<A>> list1 = faker.Create<List<List<A>>>();
            Assert.AreEqual(typeof(List<A>), list1[0].GetType());
            Assert.AreEqual(typeof(A), list1[0][0].GetType());
            Assert.AreNotEqual(0, list1[0].Count);
            Assert.AreNotEqual(0, list1.Count);
            Assert.AreNotEqual(null, list1[0][0]);

        }


        //------------CycleDependenceTests--------------

        //Book->Page->Book
        [TestMethod]
        public void CyclicalDependenceTest1()
        {
            Book book = faker.Create<Book>();
            Assert.AreEqual(null, book.PageInst.MyBook);
            Assert.AreNotEqual(null, book.PageInst);
            Assert.AreNotEqual(null, book);
        }

        //A->A
        [TestMethod]
        public void CyclicalDependenceTest2()
        {
            A a = faker.Create<A>();
            Assert.AreEqual(null, a.instA);
            Assert.AreNotEqual(null, a);
        }

        
        [TestMethod]
        public void StructCreationTest()
        {
            Time time = faker.Create<Time>();
            Assert.AreNotEqual(0, time.hours);
            Assert.AreNotEqual(0, time.minutes);
            Assert.AreNotEqual(0, time.seconds);
        }

        //Double+Long
        [TestMethod]
        public void PluginGeneratorsTest()
        {
            double d = faker.Create<double>();
            Assert.AreNotEqual<double>(0, d);
            long l = faker.Create<long>();
            Assert.AreNotEqual<long>(0, l);
        }

        [TestMethod]
        public void IsAllPluginsBeenUploadedTest()
        {
            Generator gen = new Generator();
            Assert.AreEqual(8, gen.GetAmountOfGenerators());
        }

        // Публичные поля и свойства созданного объекта не пустые
        [TestMethod]
        public void FieldsAndPropertiesFillingTest()
        {
            A a = faker.Create<A>();
            Assert.AreNotEqual(null, a.aListListPage);
            Assert.AreNotEqual(0, a.aInt); 
        }

        [TestMethod]
        public void BaseGeneratorsTest()
        {
            int a = faker.Create<int>();
            Assert.AreNotEqual(0, a);
            Assert.AreEqual(typeof(int), a.GetType());
            float b = faker.Create<float>();
            Assert.AreNotEqual(0, b);
            Assert.AreEqual(typeof(float), b.GetType());
            string s = faker.Create<string>();
            Assert.AreNotEqual(null, s);
            Assert.AreEqual(typeof(string), s.GetType());
        }

        [TestMethod]
        public void UriGeneratorTest()
        {
            Uri uri = faker.Create<Uri>();
            Assert.AreNotEqual(null, uri);
        }

        [TestMethod]
        public void StructTest()
        {
            MyStruct b = faker.Create<MyStruct>();
            Assert.AreNotEqual(null, b);
            Assert.AreNotEqual(0, b.a);
        }


        [TestMethod]
        public void StructTest1()
        {
            BBBB b = faker.Create<BBBB>();
            Assert.IsNull(b);
        }


    }
}
