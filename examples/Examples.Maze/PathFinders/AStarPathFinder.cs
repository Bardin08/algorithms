using Examples.Maze.Maze.Models;

namespace Examples.Maze.PathFinders;

internal class AStarPathFinder(bool earlyExit) : DijkstraPathFinder(earlyExit)
{
    protected override double GetWeight(Maze.Models.Maze maze, Point next, Point dest)
    {
        var w = base.GetWeight(maze, next, dest);
        return w + GetHeuristic(next, dest);
    }

    private double GetHeuristic(Point start, Point end)
        => Math.Abs(end.Column - start.Column) + Math.Abs(end.Row - start.Row);
}