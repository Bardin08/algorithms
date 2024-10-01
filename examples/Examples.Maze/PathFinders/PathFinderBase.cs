using Examples.Maze.Maze.Models;
using Examples.Maze.Observers;

namespace Examples.Maze.PathFinders;

public abstract class PathFinderBase : IPathFinder
{
    private static readonly int[] DirectionsRow = [-1, 1, 0, 0]; // Up, Down
    private static readonly int[] DirectionsCol = [0, 0, -1, 1]; // Left, Right

    private readonly List<ITypedObserver<PathFinderStats>> _observers;
    protected bool EarlyExit { get; }

    protected PathFinderBase(bool earlyExit)
    {
        EarlyExit = earlyExit;

        _observers = [];
        Subscribe(new PathFinderObserver());
    }

    public virtual List<Point> FindPath(Maze.Models.Maze maze, Point start, Point end)
    {
        var breadcrumbs = GetShortestPathInternal(maze, start, end);
        var path = RestorePath(breadcrumbs, start, end);

        var state = new PathFinderStats
        (
            Label: GetType().Name,
            NodesVisited: breadcrumbs.Count,
            PathLenght: path.Count,
            Start: start,
            End: end
        );
        Notify(state);

        return path;
    }

    private List<Point> RestorePath(
        IReadOnlyDictionary<Point, Point?> dictionary, Point start, Point dest)
    {
        var restoredPath = new List<Point>();

        var current = dest;
        while (!current.Equals(start))
        {
            restoredPath.Add(current);
            current = dictionary[current]!.Value;
        }

        restoredPath.Add(start);
        return restoredPath;
    }

    
    protected IEnumerable<Point> GetNeighbours(Maze.Models.Maze maze, Point point)
    {
        var neighbors = new List<Point>();

        for (var i = 0; i < 4; i++)
        {
            var newRow = point.Row + DirectionsRow[i];
            var newCol = point.Column + DirectionsCol[i];

            if (newCol >= 0 && newCol < maze.Structure.GetLength(0) &&
                newRow >= 0 && newRow < maze.Structure.GetLength(1) &&
                maze.Structure[newCol, newRow] is MazeTile.Space)
            {
                neighbors.Add(new Point(newCol, newRow));
            }
        }

        return neighbors;
    }

    protected abstract IReadOnlyDictionary<Point, Point?> GetShortestPathInternal(
        Maze.Models.Maze maze, Point start, Point end);

    protected abstract double GetWeight(Maze.Models.Maze maze, Point next, Point dest);

    public void Subscribe(ITypedObserver<PathFinderStats> observer) => _observers.Add(observer);
    public void Notify(PathFinderStats update) => _observers.ForEach(x => x.Handle(update));
}