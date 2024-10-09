using Lab3;

namespace Tests;

[TestFixture]
public class ThirdTaskTest
{
    
    
    //4 2
    //1 2
    //3 4
    [Test]
    public void Test1_YES()
    {
        var task = new ThirdTask(4, [1, 3], [2, 4]);
        Assert.AreEqual("YES", task.Handle());
    }

    //5 4
    //1 2
    //2 3
    //3 4
    //4 5
    [Test]
    public void Test2_YES()
    {
        var task = new ThirdTask(5, [1, 2, 3, 4], [2, 3, 4, 5]);
        Assert.AreEqual("YES", task.Handle());
    }

    //3 3
    //1 2
    //2 3
    //3 1
    [Test]
    public void Test3_NO()
    {
        var task = new ThirdTask(3, [1, 2, 3], [2, 3, 1]);
        Assert.AreEqual("NO", task.Handle());
    }
    
    //2 1
    //1 2
    [Test]
    public void Test4_YES()
    {
        var task = new ThirdTask(2, [1], [2]);
        Assert.AreEqual("YES", task.Handle());
    }
    
    //5 6
    //1 2
    //2 3
    //3 4
    //4 5
    //5 1
    //2 4
    [Test]
    public void Test5_NO()
    {
        var task = new ThirdTask(5, [1, 2, 3, 4, 5, 2], [2, 3, 4, 5, 1, 4]);
        Assert.AreEqual("NO", task.Handle());
    }
    
    //8 7
    //1 2
    //2 3
    //3 4
    //4 1
    //5 6
    //6 7
    //7 8
    [Test]
    public void Test6_YES()
    {
        var task = new ThirdTask(8, [1, 2, 3, 4, 5, 6, 7], [2, 3, 4, 1, 6, 7, 8]);
        Assert.AreEqual("YES", task.Handle());
    }
    
    //5 5
    //1 2
    //2 3
    //3 4
    //4 5
    //5 1
    [Test]
    public void Test7_NO()
    {
        var task = new ThirdTask(5, [1, 2, 3, 4, 5], [2, 3, 4, 5, 1]);
        Assert.AreEqual("NO", task.Handle());
    }
}