namespace Examples.Maze.Maze.Models;

public record struct Point(int Column, int Row)
{
    public override string ToString()
    {
        return $"r{Row}:c{Column}";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Column, Row);
    }
}