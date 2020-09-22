using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TracerLab.Tests
{
    [TestClass]
    public class TracerLabTests
    {
        [TestMethod]
        public void TestMethodSum55()
        {
            int expected = 55;
            int sum=Program.Sum55();
            Assert.AreEqual(expected,sum);
        }


    }
}
