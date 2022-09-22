using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class HelpersStatic
{
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
}
