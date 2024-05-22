using NetworkSourceSimulator;

namespace OOD_24L_01180689.src.observers;

public interface IObserver
{
    void Update(IDUpdateArgs args);
    void Update(PositionUpdateArgs args);
    void Update(ContactInfoUpdateArgs args);
}