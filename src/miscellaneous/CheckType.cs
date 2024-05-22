namespace OOD_24L_01180689.src.miscellaneous;

public class CheckType<T>
{
    public static bool Check(object obj, out T outObj)
    {
        if (obj is T)
        {
            outObj = (T)obj;
            return true;
        }

        outObj = default;
        return false;
    }
}