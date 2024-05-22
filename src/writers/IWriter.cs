namespace OOD_24L_01180689.src.writers;

public interface IWriter
{
    void Write(IEnumerable<object> objects, string dir, string filename);
}