using UnityEngine;
using System.Collections.Generic;

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count -1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    public static void Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;
    }

    public static bool CheckNulls<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                return true;
            }
        }

        return false;
    }
}