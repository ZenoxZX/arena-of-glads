using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Stats stats;

    private void Start()
    {
        stats.Initialize();

        Stats statsa = stats;
        Debug.Log(statsa.MaxHP);
        Debug.Log(statsa.MaxMP);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) stats.Initialize();
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) stats.IncreaseHP(100);
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) stats.DecreaseHP(100);
    }
}
