using GraphAlgorithms.Model;

namespace GraphAlgorithms;

using System;

public record GraphGeneratorOptions(
    int NumberOfVertices = 10,
    double EdgeCreationProbability = 0.2,
    bool IsDirected = false,
    bool IsWeighted = false,
    double MinWeight = 0.0,
    double MaxWeight = 10.0,
    int RandomSeed = 24452
);

internal class GraphGenerator<T>(GraphGeneratorOptions options)
    where T : notnull
{
    private readonly GraphGeneratorOptions _options = options;
    private readonly Random _random = new(options.RandomSeed);

    public Graph<T> Generate(Func<int, T> vertexSelector)
    {
        var graph = new Graph<T>(isDirected: _options.IsDirected);

        for (var i = 0; i < _options.NumberOfVertices; i++)
            graph.AddVertex(vertexSelector(i));

        for (var i = 0; i < _options.NumberOfVertices; i++)
        {
            for (var j = i + 1; j < _options.NumberOfVertices; j++)
            {
                if (!(_random.NextDouble() <= _options.EdgeCreationProbability))
                    continue;

                var vertex1 = vertexSelector(i);
                var vertex2 = vertexSelector(j);

                if (_options.IsWeighted)
                {
                    var weight = GetRandomWeight();
                    graph.AddEdge(vertex1, vertex2, weight);
                }
                else
                {
                    graph.AddEdge(vertex1, vertex2);
                }
            }
        }

        return graph;
    }

    private double GetRandomWeight()
        => _options.MinWeight + _random.NextDouble() * (_options.MaxWeight - _options.MinWeight);
}