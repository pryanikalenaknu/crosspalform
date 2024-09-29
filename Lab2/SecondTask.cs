using Domain;

namespace Lab2;

public class SecondTask
{
    
    public SecondTask(int[] m)
    {
        Validator.IsTrue(m.All(val => Math.Abs(val) <= 10000), "M values must be between -10000 and 10000");
        M = m;
    }
    
    private int[] M { get; }

    public long Calculate()
    {
        var dp = new int[M.Length];

        Console.WriteLine(M.Length);
        
        for (var i = 0; i < M.Length; i++)
        {
            dp[i] = 1;
        }
        
        for (var i = 1; i < M.Length; i++)
        {
            for (var j = 0; j < i; j++)
            {
                if (M[j] <= M[i])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }
        
        int maxLength = 0;
        for (int i = 0; i < M.Length; i++)
        {
            maxLength = Math.Max(maxLength, dp[i]);
        }
        
        return maxLength;
    }
    
}