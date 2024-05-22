namespace OOD_24L_01180689.src.reports;

public interface IIterator<T>
{
    bool HasNext();
    T Next();

    void Reset();
}