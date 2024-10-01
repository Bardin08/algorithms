using Examples.Maze.Maze.Models;

namespace Examples.Maze.Maze;

public class MazeGenerator
{
    private readonly MapGeneratorOptions _options;
    private readonly Random _random;
    private readonly Models.Maze _maze;

    public MazeGenerator(MapGeneratorOptions options)
    {
        _options = options;
        var seed = (int)(options.Seed == -1 ? DateTime.UtcNow.Ticks : options.Seed);
        _random = new Random(seed);

        _maze = new Models.Maze(options.Width, options.Height);
    }

    public Models.Maze GenerateMaze()
    {
        for (var x = 0; x < _options.Width; x++)
        {
            for (var y = 0; y < _options.Height; y++)
            {
                _maze.Structure[x, y] = y % 2 == 1 || x % 2 == 1 ? MazeTile.Wall : MazeTile.Space;
            }
        }

        ExpandFrom(new Point(0, 0), new List<Point>());

        RemoveWalls(_options.Noise);

        if (_options.AddTraffic)
            AddTraffic(_options.TrafficSeed);

        return _maze;
    }

    private void ExpandFrom(Point point, List<Point> visited)
    {
        visited.Add(point);
        var neighbours = GetNeighbours(point.Column, point.Row, _maze.Structure).ToArray();
        Shuffle(neighbours);

        foreach (var neighbour in neighbours)
        {
            if (visited.Contains(neighbour))
                continue;

            RemoveWallBetween(point, neighbour);
            ExpandFrom(neighbour, visited);
        }
    }

    private void RemoveWallBetween(Point a, Point b)
        => _maze.Structure[(a.Column + b.Column) / 2, (a.Row + b.Row) / 2] = MazeTile.Space;

    private void RemoveWalls(double chance)
    {
        for (var y = 0; y < _maze.Structure.GetLength(1); y++)
        {
            for (var x = 0; x < _maze.Structure.GetLength(0); x++)
            {
                if (_random.NextDouble() < chance && _maze.Structure[x, y] == MazeTile.Wall)
                {
                    _maze.Structure[x, y] = MazeTile.Space;
                }
            }
        }
    }

    private void AddTraffic(int seed)
    {
        var trafficRandom = new Random(seed);

        var nextEmpty = GetNextEmpty();
        while (nextEmpty != null)
        {
            var trafficLevel = trafficRandom.Next(1, 10);
            var trafficPath = new List<Point>();

            foreach (var point in trafficPath)
                _maze.Traffic[point.Column, point.Row] = new TrafficInfo(trafficLevel, trafficPath);

            nextEmpty = GetNextEmpty();
        }
    }

    private Point? GetNextEmpty()
    {
        for (var y = 0; y < _maze.Structure.GetLength(1); y++)
        {
            for (var x = 0; x < _maze.Structure.GetLength(0); x++)
            {
                if (_maze.Structure[x, y] == MazeTile.Space)
                    return new Point(x, y);
            }
        }

        return null;
    }

    private List<Point> GetNeighbours(int column, int row, MazeTile[,] maze, int offset = 2, bool checkWalls = false)
    {
        var result = new List<Point>();
        TryAddWithOffset(offset, 0);
        TryAddWithOffset(-offset, 0);
        TryAddWithOffset(0, offset);
        TryAddWithOffset(0, -offset);
        return result;

        void TryAddWithOffset(int offsetX, int offsetY)
        {
            var newColumn = column + offsetX;
            var newRow = row + offsetY;
            if (newColumn >= 0 && newRow >= 0 && newColumn < maze.GetLength(0) && newRow < maze.GetLength(1))
            {
                if (!checkWalls || maze[newColumn, newRow] == MazeTile.Space)
                {
                    result.Add(new Point(newColumn, newRow));
                }
            }
        }
    }

    private void Shuffle(IList<Point> array)
    {
        var n = array.Count;
        while (n > 1)
        {
            var k = _random.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}