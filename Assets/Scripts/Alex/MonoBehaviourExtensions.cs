using UnityEngine;
using System;
using System.Collections;


public static class MonoBehaviourExtensions  
{
    public static void LateStart(this MonoBehaviour mb, Action func)
    {
        mb.StartCoroutine(RunLate(func, 1));
    }

    public static void LateStart(this MonoBehaviour mb, Action func, int frames)
    {
        mb.StartCoroutine(RunLate(func, frames));
    }


    private static IEnumerator RunLate(Action func, int frames)
    {
        while (frames > 0)
        {
            frames--;
            yield return null;
        }

        func?.Invoke();
    }
}
