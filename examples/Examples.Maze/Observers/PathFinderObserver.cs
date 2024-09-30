using System.Text;

namespace Examples.Maze.Observers;

public class PathFinderObserver : ITypedObserver<PathFinderStats>
{
    public void Handle(PathFinderStats context)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Path Finder Approach: {context.Label}");
        sb.AppendLine($"Nodes Visited: {context.NodesVisited}");
        sb.AppendLine($"Start Point: ({context.Start.Column}, {context.Start.Row})");
        sb.AppendLine($"End Point: ({context.End.Column}, {context.End.Row})");
        sb.AppendLine($"Path Length: {context.PathLenght}");
        sb.AppendLine();

        Console.WriteLine(sb.ToString());
    }
}