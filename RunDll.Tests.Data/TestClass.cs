namespace RunDll
{
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
