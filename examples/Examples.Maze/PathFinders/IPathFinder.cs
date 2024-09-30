using Examples.Maze.Maze.Models;
using Examples.Maze.Observers;

namespace Examples.Maze.PathFinders;

internal interface IPathFinder : ITypedObservable<PathFinderStats>
{
    List<Point> FindPath(Maze.Models.Maze maze, Point start, Point end);
}