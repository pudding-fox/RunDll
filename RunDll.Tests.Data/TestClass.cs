namespace RunDll
{
    public interface ITestClass1
    {
        string Hello(string name);
    }

    public class TestClass1 : ITestClass1
    {
        public string Hello(string name)
        {
            return string.Concat("Hello ", name, "!");
        }
    }

    public class TestClass2
    {
        public ITestClass1 GetTarget(object config)
        {
            return new TestClass1();
        }
    }
}
