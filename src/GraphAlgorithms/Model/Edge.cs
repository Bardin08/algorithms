namespace GraphAlgorithms.Model;

internal record Edge<T>(T Source, T Destination, double? Weight = null);