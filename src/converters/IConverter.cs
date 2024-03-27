namespace OOD_24L_01180689.src.converters
{
    public interface IConverter<T, V> //TODO check if this is adapter or converter
    {
        public static abstract V Convert(T t);
    }
}