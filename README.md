A simple example (see the tests):
```c#
//Some code in a .net core app:
var mapping = new Mapping()
{
    //Map the interface to the location of the implementation.
    //The implementation is a .net framework assembly.
    { new Mapping.Key(typeof(ITestClass)), new Mapping.Value("RunDll.Tests.Data.dll", "RunDll.TestClass") }
};
using (var client = new Client<ITestClass>(Runtime.NetFramework, mapping))
{
    var expected = "Hello Test!";
    var actual = client.Target.Hello("Test");
    Assert.AreEqual(expected, actual);
}
```
Declare the interface as a copy or use multi targetting.
```c#
public interface ITestClass
{
    string Hello(string name);
}
```
The implementation.
```c#
public class TestClass : ITestClass
{
    public string Hello(string name)
    {
        return string.Concat("Hello ", name, "!");
    }
}
```
