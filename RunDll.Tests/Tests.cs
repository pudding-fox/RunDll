using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RunDll
{
    [TestClass]
    public class Tests
    {
#if NET48
        const Runtime RUNTIME = Runtime.NetCore;
#elif NET6_0_OR_GREATER
        const Runtime RUNTIME = Runtime.NetFramework;
#endif

        [TestMethod]
        public void Test001()
        {
            using (var client = new Client<ITestClass>(RUNTIME))
            {
                var expected = "Hello Test!";
                var actual = client.Target.Hello("Test");
                Assert.AreEqual(expected, actual);
            }
        }

        public interface ITestClass
        {
            string Hello(string name);
        }

        public class TestClass : ITestClass
        {
            public string Hello(string name)
            {
                return string.Concat("Hello ", name, "!");
            }
        }
    }
}
