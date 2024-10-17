namespace GraphAlgorithms.MST.Kruskal;

public class DisjointSet<T> where T : notnull
{
    private readonly Dictionary<T, T> _parent;
    private readonly Dictionary<T, int> _rank;

    public DisjointSet(IEnumerable<T> elements)
    {
        _parent = new Dictionary<T, T>();
        _rank = new Dictionary<T, int>();

        foreach (var element in elements)
        {
            _parent[element] = element;
            _rank[element] = 0;
        }
    }

    public T Find(T item)
    {
        if (!_parent[item].Equals(item))
            _parent[item] = Find(_parent[item]);

        return _parent[item];
    }

    public void Union(T item1, T item2)
    {
        var root1 = Find(item1);
        var root2 = Find(item2);

        if (root1.Equals(root2)) return;

        if (_rank[root1] > _rank[root2])
        {
            _parent[root2] = root1;
        }
        else if (_rank[root1] < _rank[root2])
        {
            _parent[root1] = root2;
        }
        else
        {
            _parent[root2] = root1;
            _rank[root1]++;
        }
    }
}