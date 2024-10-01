namespace Examples.Maze.Maze.Models;

public class Maze(int width = 100, int height = 10)
{
    public MazeTile[,] Structure { get; set; } = new MazeTile[width, height];
    public TrafficInfo[,] Traffic { get; set; } = new TrafficInfo[width, height];
}