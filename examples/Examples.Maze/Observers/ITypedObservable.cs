namespace Examples.Maze.Observers;

internal interface ITypedObservable<TReturnType>
{
    void Subscribe(ITypedObserver<TReturnType> observer);
    void Notify(TReturnType update);
}