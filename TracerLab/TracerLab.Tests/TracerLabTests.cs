using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using TracerLib.TraceUtil;

namespace TracerLab.Tests
{
    [TestClass]
    public class TracerLabTests
    {

        Tracer tracer = new Tracer();
        List<Thread> threads = new List<Thread>();

        const int expectedThreadsAmount = 3;
        const string expectedMethodName = "SomeMethod";
        const string expectedMethodClass = "TracerLabTests";

        void CreateThreads() 
        {
            for (int i=0; i< expectedThreadsAmount; i++) 
            {
                threads.Add(new Thread(SomeMethod));
                threads[i].Start();
                threads[i].Join();
            }
        }

        void SomeMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }

        [TestMethod]
        public void MethodParametrsCheck()
        {
            SomeMethod();
            Assert.AreEqual(expectedMethodName, tracer.GetTraceResult().threadSubstructures[Thread.CurrentThread.ManagedThreadId].listOfMethodSubstructures[0].MethodName);
            Assert.AreEqual(expectedMethodClass, tracer.GetTraceResult().threadSubstructures[Thread.CurrentThread.ManagedThreadId].listOfMethodSubstructures[0].ClassOfMethod);
        }

        [TestMethod]
        public void ThreadAmountCheck() 
        {
            CreateThreads();
            Assert.AreEqual(expectedThreadsAmount,tracer.GetTraceResult().threadSubstructures.Count);
        }

        [TestMethod]
        public void TestMethodSum55()
        {
            int expected = 55;
            int sum=Program.Sum55();
            Assert.AreEqual(expected,sum);
        }


    }
}
