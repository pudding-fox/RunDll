using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RunDll
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        [DataRow(Runtime.NetCore)]
        [DataRow(Runtime.NetFramework)]
        public void Test001(Runtime runtime)
        {
            var mapping = new Mapping()
            {
                { new Mapping.Key(typeof(ITestClass1)), new Mapping.Value("RunDll.Tests.Data.dll", "RunDll.TestClass1") }
            };
            using (var runner = new Runner(runtime))
            {
                var client = new Client<ITestClass1>(runner, mapping);
                var expected = "Hello Test!";
                var actual = client.Target.Hello("Test");
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        [DataRow(Runtime.NetCore)]
        [DataRow(Runtime.NetFramework)]
        public void Test002(Runtime runtime)
        {
            var mapping = new Mapping()
            {
                { new Mapping.Key(typeof(ITestClass1)), new Mapping.Value("RunDll.Tests.Data.dll", "RunDll.TestClass2") }
            };
            using (var runner = new Runner(runtime))
            {
                var client = new Client<ITestClass1>(runner, mapping);
                var expected = "Hello Test!";
                var actual = client.Target.Hello("Test");
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
