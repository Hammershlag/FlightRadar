namespace OOD_24L_01180689.src.converters;

public interface IConverter<T, V>
{
    public static abstract V Convert(T t);
}