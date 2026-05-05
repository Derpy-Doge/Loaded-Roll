

public static class ArrayExtensions
{
    
    public static void Replace<T>(this T[] array, T oldValue, T newValue)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (Equals(array[i], oldValue))
            {
                array[i] = newValue;
                return;
            }
        }
    }

    public static void Swap<T>(this T[] array, int indexA, int indexB)
    {
        if (indexA == indexB) 
        {
            return;
        }

        T temp = array[indexA];
        array[indexA] = array[indexB];
        array[indexB] = temp;
    }
}
