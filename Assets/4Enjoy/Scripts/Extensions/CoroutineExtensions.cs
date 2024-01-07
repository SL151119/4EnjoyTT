using System;
using System.Collections;
using UnityEngine;

public static class CoroutineExtensions
{
    public static void UniversalWait(this MonoBehaviour monoBehaviour, float time, Action action)
        => monoBehaviour.StartCoroutine(UniversalWaitCoroutine(time, action));

    private static IEnumerator UniversalWaitCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
