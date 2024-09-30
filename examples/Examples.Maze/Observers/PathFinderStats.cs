using Examples.Maze.Maze.Models;

namespace Examples.Maze.Observers;

public record PathFinderStats(
    string Label,
    int NodesVisited,
    int PathLenght,
    Point Start,
    Point End);