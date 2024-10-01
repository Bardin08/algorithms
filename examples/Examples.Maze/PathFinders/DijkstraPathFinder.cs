using Examples.Maze.Maze.Models;

namespace Examples.Maze.PathFinders;

internal class DijkstraPathFinder(bool earlyExit) : PathFinderBase(earlyExit)
{
    protected override IReadOnlyDictionary<Point, Point?> GetShortestPathInternal(
        Maze.Models.Maze maze, Point start, Point end)
    {
        var frontier = new PriorityQueue<Point, double>();
        var path = new Dictionary<Point, Point?>();
        var costs = new Dictionary<Point, double>();

        path[start] = default;
        costs[start] = double.NegativeZero;
        frontier.Enqueue(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            var neighbours = GetNeighbours(maze, current);

            if (EarlyExit && current.Equals(end))
                break;

            foreach (var next in neighbours.Where(next => !path.ContainsKey(next)))
            {
                var newCost = costs[current] + GetWeight(maze, next, end);
                if (costs.TryGetValue(next, out var value) && newCost >= value)
                    continue;

                costs[next] = newCost;
                frontier.Enqueue(next, newCost);
                path[next] = current;
            }
        }

        return path;
    }

    protected override double GetWeight(Maze.Models.Maze maze, Point next, Point dest)
    {
        try
        {
            var trafficInfo = maze.Traffic[next.Row, next.Column];
            return trafficInfo.TrafficLevel;
        }
        catch
        {
            return 1;
        }
    }
}