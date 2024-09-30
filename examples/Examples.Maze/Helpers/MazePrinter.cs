using System.Text;
using Examples.Maze.Maze.Models;

namespace Examples.Maze.Helpers;

public static class MazePrinter
{
    public static StringBuilder GetFilledStringBuilder(
        Maze.Models.Maze maze,
        List<Point> points,
        Point? start = null,
        Point? end = null)
    {
        var sb = new StringBuilder();
        var displayMaze = new string[maze.Structure.GetLength(0), maze.Structure.GetLength(1)];

        for (var row = 0; row < maze.Structure.GetLength(1); row++)
        {
            for (var column = 0; column < maze.Structure.GetLength(0); column++)
            {
                displayMaze[column, row] = ConvertTileToChar(maze.Structure[column, row]);
            }
        }

        points.ForEach(p => displayMaze[p.Column, p.Row] = "·");
        if (start != null) displayMaze[start.Value.Column, start.Value.Row] = "S";
        if (end != null) displayMaze[end.Value.Column, end.Value.Row] = "X";

        PrintTopLine(sb);

        for (var row = 0; row < displayMaze.GetLength(1); row++)
        {
            sb.Append($"{row}\t");
            for (var column = 0; column < displayMaze.GetLength(0); column++)
            {
                sb.Append(displayMaze[column, row]);
            }

            sb.AppendLine();
        }

        return sb;

        string ConvertTileToChar(MazeTile tile)
        {
            return tile switch
            {
                MazeTile.Wall => "█",
                MazeTile.Space => " ",
                MazeTile.Start => "S",
                MazeTile.Exit => "X",
                MazeTile.Traffic => "T",
                _ => "?"
            };
        }

        void PrintTopLine(StringBuilder stringBuilder)
        {
            stringBuilder.Append(" \t");
            for (var i = 0; i < displayMaze.GetLength(0); i++)
            {
                stringBuilder.Append(i % 10 == 0 ? i / 10 : " ");
            }

            stringBuilder.Append("\n \t");
            for (var i = 0; i < displayMaze.GetLength(0); i++)
            {
                stringBuilder.Append(i % 10);
            }

            stringBuilder.AppendLine("\n");
        }
    }
}