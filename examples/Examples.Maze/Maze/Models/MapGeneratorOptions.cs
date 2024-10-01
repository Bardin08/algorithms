namespace Examples.Maze.Maze.Models;

public record MapGeneratorOptions(
    int Width = 100,
    int Height = 10,
    int Seed = 12345,
    double Noise = 0.1d,
    bool AddTraffic = false,
    int TrafficSeed = 12345
);