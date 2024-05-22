namespace OOD_24L_01180689.src.observers;

public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
}