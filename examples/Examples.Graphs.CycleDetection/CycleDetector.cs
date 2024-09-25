using GraphAlgorithms.Model;

namespace Examples.Graphs.CycleDetection;

    internal class CycleDetector<T> where T : notnull
    {
        private Graph<T> _graph = null!;
        private Dictionary<T, int> _color = null!;  // 0: White, 1: Gray, 2: Black
        private Dictionary<T, T?> _parent = null!;  // Tracks parent for cycle reconstruction
        private List<List<T>> _cycles = null!;      // List of cycles

        public List<List<T>> FindAllCycles(Graph<T> graph)
        {
            SetInitialState(graph);

            foreach (var vertex in _graph.AdjacencyList.Keys)
            {
                if (_color[vertex] == 0)
                {
                    DetectCyclesInternal(vertex, default);
                }
            }
            return _cycles;
        }

        private void DetectCyclesInternal(T vertex, T? parent)
        {
            _color[vertex] = 1;  // Gray: This vertex is being processed

            foreach (var edge in _graph.GetNeighbors(vertex))
            {
                var neighbor = edge.Destination;

                // Ignore the edge back to the parent to avoid false cycle detection
                if (neighbor.Equals(parent) && !_graph.IsDirected)
                {
                    continue;
                }

                if (_color[neighbor] == 1)  // Back edge: cycle detected
                {
                    TrackCycle(vertex, neighbor);
                }
                else if (_color[neighbor] == 0)  // White: continue DFS
                {
                    _parent[neighbor] = vertex;
                    DetectCyclesInternal(neighbor, vertex);
                }
            }

            _color[vertex] = 2;  // Black: Fully processed
        }

        private void TrackCycle(T vertex, T neighbor)
        {
            var cycle = new List<T> { neighbor };
            var current = vertex;

            // Reconstruct the cycle path
            while (current != null && !current.Equals(neighbor))
            {
                cycle.Add(current);
                current = _parent[current];
            }
            cycle.Add(current!);

            cycle.Reverse();
            _cycles.Add(cycle);
        }

        private void SetInitialState(Graph<T> graph)
        {
            _graph = graph;
            _color = new Dictionary<T, int>();
            _parent = new Dictionary<T, T?>();
            _cycles = new List<List<T>>();
            
            // Initialize all vertices as White (unvisited)
            foreach (var vertex in _graph.AdjacencyList.Keys)
            {
                _color[vertex] = 0;  // White
                _parent[vertex] = default;
            }
        }
    }
