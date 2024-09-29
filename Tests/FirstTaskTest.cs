using Domain;
using Lab1;
using ArgumentException = System.ArgumentException;

namespace Tests;

[TestFixture]
public class FirstTaskTest
{

    private Random _random;
    private int N;
    private int[] M;
    
    [SetUp]
    public void SetUp()
    {
        _random = new Random();
        
        N = _random.Next(1, 1000);
        M = new int[N];

        for (var i = 0; i < N; i++)
        {
            M[i] = _random.Next(1, 1000);
        }
    }
    
    [Test]
    public void TestRandomValuesNoException()
    {
        var firstTask = new FirstTask(N, M);
        firstTask.Calculate();
    }
    
    [Test]
    public void TestDefaultValue()
    {
        var firstTask = new FirstTask(3, [100, 100, 100]);
        var result = firstTask.Calculate();
        
        Validator.IsTrue(result == 1000000, "");
    }
    
    [Test]
    public void TestDefaultValue2()
    {
        var firstTask = new FirstTask(4, [2, 1, 1, 1]);
        var result = firstTask.Calculate();
        
        Validator.IsTrue(result == 7, "");
    }

    [Test]
    public void TestNValueValidation()
    {
        Assert.Throws<ArgumentException>(code: () => new FirstTask(0, []));
    }
    
    [Test]
    public void TestMValueValidation()
    {
        Assert.Throws<ArgumentException>(code: () => new FirstTask(4, [100, 3213, 3244, 4144, 444]));
    }
}