namespace GraphAlgorithms.MST.Prim;

public class MinHeap<T>(Comparison<T> comparison)
{
    private readonly List<T> _elements = [];
    private readonly Comparison<T> _comparison = comparison;

    public int Count => _elements.Count;

    public void Insert(T element)
    {
        _elements.Add(element);
        HeapifyUp(_elements.Count - 1);
    }

    public T ExtractMin()
    {
        if (_elements.Count == 0)
            throw new InvalidOperationException("Heap is empty.");

        var minElement = _elements[0];
        _elements[0] = _elements[^1];
        _elements.RemoveAt(_elements.Count - 1);
        HeapifyDown(0);

        return minElement;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            var parentIndex = (index - 1) / 2;
            if (_comparison(_elements[index], _elements[parentIndex]) >= 0)
                break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        while (true)
        {
            var leftChild = 2 * index + 1;
            var rightChild = 2 * index + 2;
            var smallest = index;

            if (leftChild < _elements.Count && _comparison(_elements[leftChild], _elements[smallest]) < 0)
                smallest = leftChild;

            if (rightChild < _elements.Count && _comparison(_elements[rightChild], _elements[smallest]) < 0)
                smallest = rightChild;

            if (smallest == index)
                break;

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void Swap(int idx1, int idx2)
        => (_elements[idx1], _elements[idx2]) = (_elements[idx2], _elements[idx1]);
}