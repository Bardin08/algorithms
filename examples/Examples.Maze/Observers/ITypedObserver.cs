namespace Examples.Maze.Observers;

public interface ITypedObserver<in TContext>
{
    public void Handle(TContext context);
}