namespace GraphAlgorithms.Model;

public record Edge<T>(T Source, T Destination, double? Weight = null);