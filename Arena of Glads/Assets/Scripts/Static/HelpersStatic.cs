using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class HelpersStatic
{
    #region Shuffle Generic List

    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    #endregion

    #region Get Random Index By Percentages

    public static int GetRandomIndexByPercentages(params float[] percentages)
    {
        int percentagesLength = percentages.Length;
        float maxPercentage = 0;
        float[] sums = new float[percentagesLength + 1];
        sums[0] = 0;

        for (int i = 0; i < percentagesLength; i++)
        {
            maxPercentage += percentages[i];
            sums[i + 1] = maxPercentage;
        }
        float randomNumber = Random.Range(0, maxPercentage);

        for (int i = 0; i < percentagesLength; i++)
        {
            if (randomNumber > sums[i] && randomNumber < sums[i + 1]) return i;
        }

        return 0;
    }

    #endregion

    #region IsOverUI

    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;

    public static bool IsOverUI
    {
        get
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
            return _results.Count > 0;
        }
    }

    #endregion

    #region Non-Alloc WaitForSeconds

    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitForSeconds(float time)
    {
        if (WaitDictionary.TryGetValue(time, out WaitForSeconds value)) return value;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    #endregion
}
