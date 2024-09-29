using Lab2;

namespace Tests;

[TestFixture]
public class SecondTaskTest
{
    
    [Test]
    public void Test_SampleData_ShouldReturn3()
    {
        var input = new[]{63, 29, 5, 5, 28, 6};

        var secondTask = new SecondTask(input);
        Assert.AreEqual(3, secondTask.Calculate());
    }

    [Test]
    public void Test_AllIncreasing_ShouldReturnN()
    {
        var input = new[]{1, 2, 3, 4, 5};

        var secondTask = new SecondTask(input);
        Assert.AreEqual(5, secondTask.Calculate());
    }

    [Test]
    public void Test_AllSame_ShouldReturnN()
    {
        var input = new[]{7, 7, 7, 7, 7};

        var secondTask = new SecondTask(input);
        Assert.AreEqual(5, secondTask.Calculate());
    }

    [Test]
    public void Test_DecreasingSequence_ShouldReturn1()
    {
        var input = new[]{5, 4, 3, 2, 1};

        var secondTask = new SecondTask(input);
        Assert.AreEqual(1, secondTask.Calculate());
    }

    [Test]
    public void Test_RandomSequence_ShouldReturnCorrectResult()
    {
        var input = new[]{10, 22, 9, 33, 21, 50, 41};

        var secondTask = new SecondTask(input);
        Assert.AreEqual(4, secondTask.Calculate());
    }
    
}