namespace Lab3;

public class ThirdTask
{

    private readonly int _n;
    private readonly List<int> _firstTarget;
    private readonly List<int> _secodnTarget;

    public ThirdTask(int n, List<int> firstTarget, List<int> secodnTarget)
    {
        _n = n;
        _firstTarget = firstTarget;
        _secodnTarget = secodnTarget;
    }

    public string Handle()
    {
        var graph = new VIPGraph(_n);

        for (var i = 0; i < _firstTarget.Count; i++)
        {
            graph.AddEdge(_firstTarget[i], _secodnTarget[i]);
        }

        return graph.IsBipartite() ? "YES" : "NO";
    }
}