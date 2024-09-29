using Domain;

namespace Lab1;

public class FirstTask
{
    
    public FirstTask(int n, int[] m)
    {
        Validator.IsTrue(n is >= 3 and <= 1000, "N must be between 1 and 1000");
        Validator.IsTrue(m.All(val => val is >= 1 and <= 1000), "M values must be between 1 and 1000");
        
        N = n;
        M = m;
    }
    
    private int N { get; }
    private int[] M { get; }

    public long Calculate()
    {
        var result = 0L;

        for (var i = 0; i < N; i++)
        {
            for (var j = i + 1; j < N; j++)
            {
                for (var k = j + 1; k < N; k++)
                {
                    result += (long)M[i] * M[j] * M[k];
                }
            }
        }

        return result;
    }
    
}