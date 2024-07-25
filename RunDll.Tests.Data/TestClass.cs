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

    public class TestClass3Request
    {
        public string Name { get; set; }
    }

    public class TestClass3Response
    {
        public string Message { get; set; }
    }

    public interface ITestClass3
    {
        TestClass3Response Hello(TestClass3Request request);
    }

    public class TestClass3 : ITestClass3
    {
        public TestClass3Response Hello(TestClass3Request request)
        {
            return new TestClass3Response()
            {
                Message = string.Concat("Hello ", request.Name, "!")
            };
        }
    }
}
