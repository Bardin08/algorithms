using Examples.Graphs.CycleDetection;
using GraphAlgorithms.Model;

var graph = new Graph<int>(isDirected: false);

// Add vertices and edges
graph.AddVertex(0);
graph.AddVertex(1);
graph.AddVertex(6);
graph.AddVertex(9);

graph.AddVertex(2);

graph.AddEdge(0, 1);
graph.AddEdge(1, 6);
graph.AddEdge(6, 9);
graph.AddEdge(9, 0);

graph.AddEdge(1, 2);
graph.AddEdge(2, 9);

var cycleDetector = new CycleDetector<int>();
// Console.WriteLine("Graph contains cycle: " + cycleDetector.HasCycle(graph));

var cycles = cycleDetector.FindAllCycles(graph);

foreach (var cycle in cycles)
{
    Console.WriteLine("Cycle detected: " + string.Join(" -> ", cycle));
}
