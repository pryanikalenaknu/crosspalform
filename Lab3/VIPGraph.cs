namespace Lab3;

public class VIPGraph
{

    private readonly Stack<int> _checkStack = [];
    private readonly List<int>[] _adjList;
    private readonly int[] _trigger;
    private readonly int _n;
    
    private const int No = 0;
    private const int Yes = 0;

    
    public VIPGraph(int n)
    {
        _n = n;
        _trigger = new int[_n + 1];
        _adjList = new List<int>[_n + 1];
        for (var i = 0; i <= _n; i++)
        {
            _adjList[i] = [];
        }
    }
    
    public void AddEdge(int u, int v)
    {
        _adjList[u].Add(v);
        _adjList[v].Add(u);
    }
    
    public bool IsBipartite()
    {
        for (var i = 1; i <= _n; i++)
        {
            if (_trigger[i] != No) continue;
            if (!HandleCheck(i))
            {
                return false;
            }
        }
        return true;
    }

    private bool HandleCheck(int startNode)
    {
        _checkStack.Push(startNode);
        _trigger[startNode] = 1;

        while (_checkStack.Count > 0)
        {
            var node = _checkStack.Pop();

            foreach (var neighbor in _adjList[node])
            {
                if (_trigger[neighbor] == 0)
                {
                    _trigger[neighbor] = -_trigger[node];
                    _checkStack.Push(neighbor);
                    continue;
                }
                
                if (_trigger[neighbor] == _trigger[node])
                {
                    return false;
                }
            }
        }

        return true;
    }
}