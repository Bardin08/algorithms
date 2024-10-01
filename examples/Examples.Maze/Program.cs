using Examples.Maze.Helpers;
using Examples.Maze.Maze;
using Examples.Maze.Maze.Models;
using Examples.Maze.PathFinders;

var maze = new MazeGenerator(new MapGeneratorOptions())
    .GenerateMaze();

var startPoint = new Point(0, 0);
var endPoint = new Point(81, 3);

var pathFinder = new DijkstraPathFinder(earlyExit: true);
var path = pathFinder.FindPath(maze, startPoint, endPoint);

Console.WriteLine();

var sb = MazePrinter.GetFilledStringBuilder(maze, path, startPoint, endPoint);
Console.WriteLine(sb.ToString());

Console.WriteLine();

var aStarFinder = new AStarPathFinder(earlyExit: true);
var aStarPath = aStarFinder.FindPath(maze, startPoint, endPoint);

Console.WriteLine();

sb = MazePrinter.GetFilledStringBuilder(maze, aStarPath, startPoint, endPoint);
Console.WriteLine(sb.ToString());

Console.WriteLine();