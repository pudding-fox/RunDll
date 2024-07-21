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
                { new Mapping.Key(typeof(ITestClass)), new Mapping.Value("RunDll.Tests.Data.dll", "RunDll.TestClass") }
            };
            using (var client = new Client<ITestClass>(runtime, mapping))
            {
                var expected = "Hello Test!";
                var actual = client.Target.Hello("Test");
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
