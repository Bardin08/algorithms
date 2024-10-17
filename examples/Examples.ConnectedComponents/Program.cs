using Examples.ConnectedComponents;
using GraphAlgorithms;
using GraphAlgorithms.Helpers;
using GraphAlgorithms.Traversals.Bfs;

var graph = new GraphGenerator<int>(new GraphGeneratorOptions()).Generate(x => x);

graph.PrintGraph();

var componentsFinder = new ConnectedComponentsFinder();
var traversalStrategy = new BreadthFirstSearchIterative<int>();

var components = await componentsFinder.FindConnectedComponents(graph, traversalStrategy);

foreach (var component in components)
{
    Console.WriteLine(string.Join("-", component));
}